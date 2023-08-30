using Projects.Query.Api.Interfaces;
using Projects.Query.Api.Queries;
using Projects.Query.Domain.Entities;
using Projects.Query.Domain.Interfaces;

namespace Projects.Query.Api.Handlers
{
    public class ProjectQueryHandler : IProjectQueryHandler
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectEntity>> HandleAsync(FindAllProjectsQuery query)
        {
            return await _projectRepository.ListAllAsync();
        }

        public async Task<List<ProjectEntity>> HandleAsync(FindProjectByIdQuery query)
        {
            var project = await _projectRepository.GetByIdAsync(query.Id);
            return new List<ProjectEntity> { project };
        }

        public async Task<List<ProjectEntity>> HandleAsync(FindProjectByCodeQuery query)
        {
            var project = await _projectRepository.GetByCodeAsync(query.Code);
            return new List<ProjectEntity> { project };
        }

        public async Task<List<ProjectEntity>> HandleAsync(FindDeletedProjectsQuery query)
        {
            return await _projectRepository.ListDeletedAsync();
        }
    }
}