using Projects.Query.Api.Queries;
using Projects.Query.Domain.Entities;

namespace Projects.Query.Api.Interfaces
{
    public interface IProjectQueryHandler
    {
        Task<List<ProjectEntity>> HandleAsync(FindAllProjectsQuery query);
        Task<List<ProjectEntity>> HandleAsync(FindDeletedProjectsQuery query);
        Task<List<ProjectEntity>> HandleAsync(FindProjectByIdQuery query);
        Task<List<ProjectEntity>> HandleAsync(FindProjectByCodeQuery query);
    }
}
