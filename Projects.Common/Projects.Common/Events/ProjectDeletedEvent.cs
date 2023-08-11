using CQRS.Core.Events;

namespace Projects.Common.Events
{
    public class ProjectDeletedEvent : BaseEvent
    {
        public ProjectDeletedEvent() : base(nameof(ProjectDeletedEvent))
        {
        }
    }
}
