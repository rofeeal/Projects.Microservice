using CQRS.Core.Handlers;
using Projects.Cmd.Api.Commands;
using Projects.Cmd.Api.Interfaces;
using Projects.Cmd.Domain.Aggregates;

namespace Projects.Cmd.Api.Handlers
{
    public class ProjectCommandHandler : IProjectCommandHandler
    {
        private readonly IEventSourcingHandler<ProjectAggregate> _eventSourcingHandler;

        public ProjectCommandHandler(IEventSourcingHandler<ProjectAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task HandleAsync(NewProjectCommand command)
        {
            var aggregate = new ProjectAggregate(
                id: command.Id,
                code: command.Code,
                parentID: command.ParentID,
                isParent: command.IsParent,
                name: command.Name,
                priority: command.Priority,
                workStatus: command.WorkStatus,
                clientId: command.ClientId,
                price: command.Price,
                startDate: command.StartDate,
                endDate: command.EndDate,
                description: command.Description,
                image: command.Image,
                active: command.Active
            );

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(EditProjectCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.EditProjectAggregate(
                id : command.Id,
                code: command.Code,
                parentID: command.ParentID,
                isParent: command.IsParent,
                name: command.Name,
                priority: command.Priority,
                workStatus: command.WorkStatus,
                clientId: command.ClientId,
                price: command.Price,
                startDate: command.StartDate,
                endDate: command.EndDate,
                description: command.Description,
                image: command.Image,
                active: command.Active
            );

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(DeleteProjectCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.DeleteProject(command.Id);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(DeleteProjectPermanentlyCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.DeleteProjectPermanently(command.Id);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(RestoreReadDbProjectCommand command)
        {
            await _eventSourcingHandler.RepublishEventsAsync(command.AggregateType);
        }
    }
}