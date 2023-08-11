using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Interfaces;
using CQRS.Core.Producers;
using MongoDB.Bson.Serialization;
using Projects.Cmd.Api.Commands;
using Projects.Cmd.Api.Handlers;
using Projects.Cmd.Api.Interfaces;
using Projects.Cmd.Domain.Aggregates;
using Projects.Cmd.Infrastructure.Config;
using Projects.Cmd.Infrastructure.Dispatchers;
using Projects.Cmd.Infrastructure.Handlers;
using Projects.Cmd.Infrastructure.Producers;
using Projects.Cmd.Infrastructure.Repositories;
using Projects.Cmd.Infrastructure.Stores;
using Projects.Common.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BsonClassMap.RegisterClassMap<BaseEvent>();
BsonClassMap.RegisterClassMap<ProjectCreatedEvent>();
BsonClassMap.RegisterClassMap<ProjectEditedEvent>();
BsonClassMap.RegisterClassMap<ProjectDeletedEvent>();
BsonClassMap.RegisterClassMap<ProjectPermanentlyDeletedEvent>();


var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env.Equals("Development"))
{
    builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
    builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
}
else
{
    var connecionString = $"mongodb://{builder.Configuration["MONGODB_HOST"]}:{builder.Configuration["MONGODB_PORT"]}";
    Console.WriteLine(connecionString);
    // Add services to the container.
    builder.Services.Configure<MongoDbConfig>(options =>
    {
        options.ConnectionString = connecionString;
        options.Database = builder.Configuration["MONGODB_DATABASE"];
        options.Collection = builder.Configuration["MONGODB_COLLECTION"];
    });


    builder.Services.Configure<ProducerConfig>(options =>
    {
        options.BootstrapServers = builder.Configuration["PRODUCER_BOOTSTRAP_SERVERS"];
    });
}

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<ProjectAggregate>, ProjectEventSourcingHandler>();
builder.Services.AddScoped<IProjectCommandHandler, ProjectCommandHandler>();

// register command handler methods
var accountCommandHandler = builder.Services.BuildServiceProvider().GetRequiredService<IProjectCommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<NewProjectCommand>(accountCommandHandler.HandleAsync);
dispatcher.RegisterHandler<EditProjectCommand>(accountCommandHandler.HandleAsync);
dispatcher.RegisterHandler<DeleteProjectCommand>(accountCommandHandler.HandleAsync);
dispatcher.RegisterHandler<DeleteProjectPermanentlyCommand>(accountCommandHandler.HandleAsync);
dispatcher.RegisterHandler<RestoreReadDbProjectCommand>(accountCommandHandler.HandleAsync);

builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);


builder.Services.AddControllers();
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
