using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projects.Query.Domain.Entities
{
    [Table("ProjectStatus", Schema = "dbo")]
    public class ProjectStatusEntity
    {
        [Key]
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
