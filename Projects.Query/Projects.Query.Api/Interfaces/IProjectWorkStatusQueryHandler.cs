using Projects.Query.Api.Queries;
using Projects.Query.Domain.Entities;

namespace Projects.Query.Api.Interfaces
{
    public interface IProjectStatusQueryHandler
    {
        Task<List<ProjectStatusEntity>> HandleAsync(FindProjectStatusListQuery query);
    }
}
