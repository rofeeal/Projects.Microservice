using CQRS.Core.Events;
using Projects.Common.Enum;

namespace Projects.Common.Events
{
    public class ProjectEditedEvent : BaseEvent
    {
        public ProjectEditedEvent() : base(nameof(ProjectEditedEvent))
        {
        }
        public int Code { get; set; }
        public Guid ParentID { get; set; }
        public bool IsParent { get; set; } = false;
        public string Name { get; set; }
        public ProjectPriority Priority { get; set; }
        public ProjectWorkStatus WorkStatus { get; set; }
        public Guid? ClientId { get; set; }
        public float? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool Active { get; set; }
    }
}
