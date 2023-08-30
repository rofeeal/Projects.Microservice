using Projects.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Projects.Query.Infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ProjectPriorityEntity> ProjectPriorities { get; set; }
        public DbSet<ProjectStatusEntity> ProjectStatuses { get; set; }
    }
}