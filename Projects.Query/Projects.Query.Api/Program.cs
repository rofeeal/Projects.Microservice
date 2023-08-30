using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Projects.Query.Api.Handlers;
using Projects.Query.Api.Interfaces;
using Projects.Query.Api.Queries;
using Projects.Query.Domain.Entities;
using Projects.Query.Domain.Interfaces;
using Projects.Query.Infrastructure.Consumers;
using Projects.Query.Infrastructure.DataAccess;
using Projects.Query.Infrastructure.Dispatchers;
using Projects.Query.Infrastructure.Handlers;
using Projects.Query.Infrastructure.Interfaces;
using Projects.Query.Infrastructure.Repositories;
using Projects.Query.Infrastructure.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Action<DbContextOptionsBuilder> configureDbContext;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env.Equals("Development"))
{
    configureDbContext = o => o.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("SqlServer"));
}
else
{
    configureDbContext = o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
}

builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

// create database and tables
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();
bool useFakeData = builder.Configuration.GetValue<bool>("FakeData");
SeedData.Initialize(dataContext);
if (useFakeData)
{
    FakeData.Initialize(dataContext);
}
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectPriorityRepository, ProjectPriorityRepository>();
builder.Services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
builder.Services.AddScoped<IProjectQueryHandler, ProjectQueryHandler>();
builder.Services.AddScoped<IProjectPriorityQueryHandler, ProjectPriorityQueryHandler>();
builder.Services.AddScoped<IProjectStatusQueryHandler, ProjectStatusQueryHandler>();
builder.Services.AddScoped<IEventHandler, AllEventHandler>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

// register query handler methods
var projectQueryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IProjectQueryHandler>();
var projectPriorityQueryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IProjectPriorityQueryHandler>();
var projectStatusQueryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IProjectStatusQueryHandler>();

var projectDispatcher = new ProjectQueryDispatcher();
var projectPriorityDispatcher = new ProjectPriorityQueryDispatcher();
var projectStatusDispatcher = new ProjectStatusQueryDispatcher();

projectDispatcher.RegisterHandler<FindAllProjectsQuery>(projectQueryHandler.HandleAsync);
projectDispatcher.RegisterHandler<FindDeletedProjectsQuery>(projectQueryHandler.HandleAsync);
projectDispatcher.RegisterHandler<FindProjectByIdQuery>(projectQueryHandler.HandleAsync);
projectDispatcher.RegisterHandler<FindProjectByCodeQuery>(projectQueryHandler.HandleAsync);

projectPriorityDispatcher.RegisterHandler<FindProjectPriorityListQuery>(projectPriorityQueryHandler.HandleAsync);
projectStatusDispatcher.RegisterHandler<FindProjectStatusListQuery>(projectStatusQueryHandler.HandleAsync);

builder.Services.AddScoped<IQueryDispatcher<ProjectEntity>>(_ => projectDispatcher);
builder.Services.AddScoped<IQueryDispatcher<ProjectPriorityEntity>>(_ => projectPriorityDispatcher);
builder.Services.AddScoped<IQueryDispatcher<ProjectStatusEntity>>(_ => projectStatusDispatcher);

builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
