using Projects.Common.Events;

namespace Projects.Query.Infrastructure.Interfaces
{
    public interface IEventHandler
    {
        Task On(ProjectCreatedEvent @event);
        Task On(ProjectEditedEvent @event);
        Task On(ProjectDeletedEvent @event);
        Task On(ProjectPermanentlyDeletedEvent @event);
    }
}
