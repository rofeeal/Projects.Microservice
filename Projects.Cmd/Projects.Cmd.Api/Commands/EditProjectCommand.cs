using CQRS.Core.Commands;

namespace Projects.Cmd.Api.Commands
{
    public class EditProjectCommand : BaseCommand
    {
        public int Code { get; set; }
        public Guid ParentId { get; set; }
        public bool IsParent { get; set; }
        public string Name { get; set; }
        public Guid PriorityId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? ClientId { get; set; }
        public float? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool Active { get; set; }
    }
}
