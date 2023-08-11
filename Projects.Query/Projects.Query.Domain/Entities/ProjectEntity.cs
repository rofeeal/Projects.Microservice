using Projects.Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projects.Query.Domain.Entities
{
    [Table("Project", Schema = "dbo")]
    public class ProjectEntity : AuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid ParentID { get; set; } = Guid.Empty;
        public Guid RootID { get; set; } = Guid.Empty;
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
        public bool Active { get; set; } = true;
    }
}
