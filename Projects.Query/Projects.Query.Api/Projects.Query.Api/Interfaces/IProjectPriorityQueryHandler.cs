using Projects.Query.Api.Queries;
using Projects.Query.Domain.Enum;

namespace Projects.Query.Api.Interfaces
{
    public interface IProjectPriorityQueryHandler
    {
        Task<List<ProjectPriorityEnum>> HandleAsync(FindProjectPriorityListQuery query);
    }
}
