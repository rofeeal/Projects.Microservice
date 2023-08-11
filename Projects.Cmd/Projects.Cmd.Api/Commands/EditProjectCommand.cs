using CQRS.Core.Commands;
using Projects.Common.Enum;

namespace Projects.Cmd.Api.Commands
{
    public class EditProjectCommand : BaseCommand
    {
        public int Code { get; set; }
        public Guid ParentID { get; set; }
        public bool IsParent { get; set; }
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
