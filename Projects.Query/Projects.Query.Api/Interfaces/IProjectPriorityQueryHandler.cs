using Projects.Query.Api.Queries;
using Projects.Query.Domain.Entities;

namespace Projects.Query.Api.Interfaces
{
    public interface IProjectPriorityQueryHandler
    {
        Task<List<ProjectPriorityEntity>> HandleAsync(FindProjectPriorityListQuery query);
    }
}
