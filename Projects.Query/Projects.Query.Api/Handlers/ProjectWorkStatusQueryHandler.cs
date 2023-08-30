using Projects.Query.Api.Interfaces;
using Projects.Query.Api.Queries;
using Projects.Query.Domain.Entities;
using Projects.Query.Domain.Interfaces;

namespace Projects.Query.Api.Handlers
{
    public class ProjectStatusQueryHandler : IProjectStatusQueryHandler
    {
        private readonly IProjectStatusRepository _projectStatusRepository;

        public ProjectStatusQueryHandler(IProjectStatusRepository projectStatusRepository)
        {
            _projectStatusRepository = projectStatusRepository;
        }

        public async Task<List<ProjectStatusEntity>> HandleAsync(FindProjectStatusListQuery query)
        {
            return await _projectStatusRepository.ListAllAsync();
        }
    }
}
