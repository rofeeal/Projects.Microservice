using Microsoft.EntityFrameworkCore;
using Projects.Query.Domain.Entities;
using Projects.Query.Domain.Interfaces;
using Projects.Query.Infrastructure.DataAccess;

namespace Projects.Query.Infrastructure.Repositories
{
    public class ProjectPriorityRepository : IProjectPriorityRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public ProjectPriorityRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<ProjectPriorityEntity>> ListAllAsync()
        {
            try
            {
                using (DatabaseContext context = _contextFactory.CreateDbContext())
                {
                    return await context.ProjectPriorities
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the undeleted project priorities list.", ex);
            }
        }
    }
}
