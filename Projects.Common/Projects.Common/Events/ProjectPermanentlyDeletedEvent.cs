using CQRS.Core.Events;

namespace Projects.Common.Events
{
    public class ProjectPermanentlyDeletedEvent : BaseEvent
    {
        public ProjectPermanentlyDeletedEvent() : base(nameof(ProjectPermanentlyDeletedEvent))
        {
        }
    }
}
