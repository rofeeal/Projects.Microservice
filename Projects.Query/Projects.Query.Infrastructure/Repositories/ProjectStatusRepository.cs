using Microsoft.EntityFrameworkCore;
using Projects.Query.Domain.Entities;
using Projects.Query.Domain.Interfaces;
using Projects.Query.Infrastructure.DataAccess;

namespace Projects.Query.Infrastructure.Repositories
{
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public ProjectStatusRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<ProjectStatusEntity>> ListAllAsync()
        {
            try
            {
                using (DatabaseContext context = _contextFactory.CreateDbContext())
                {
                    return await context.ProjectStatuses
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the undeleted project statuses list.", ex);
            }
        }
    }
}