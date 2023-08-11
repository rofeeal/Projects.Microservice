using Projects.Query.Api.Interfaces;
using Projects.Query.Api.Queries;
using Projects.Query.Domain.Enum;
using Projects.Query.Domain.Interfaces;

namespace Projects.Query.Api.Handlers
{
    public class ProjectWorkStatusQueryHandler : IProjectWorkStatusQueryHandler
    {
        private readonly IProjectWorkStatusRepository _projectWorkStatusRepository;

        public ProjectWorkStatusQueryHandler(IProjectWorkStatusRepository projectWorkStatusRepository)
        {
            _projectWorkStatusRepository = projectWorkStatusRepository;
        }

        public async Task<List<ProjectWorkStatusEnum>> HandleAsync(FindProjectWorkStatusListQuery query)
        {
            return await _projectWorkStatusRepository.ListAllAsync();
        }
    }
}
