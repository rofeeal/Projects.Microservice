
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projects.Query.Domain.Entities
{
    [Table("ProjectPriority", Schema = "dbo")]
    public class ProjectPriorityEntity
    {
        [Key]
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
