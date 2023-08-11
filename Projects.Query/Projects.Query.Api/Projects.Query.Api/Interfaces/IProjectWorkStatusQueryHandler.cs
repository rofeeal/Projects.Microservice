using Projects.Query.Api.Queries;
using Projects.Query.Domain.Enum;

namespace Projects.Query.Api.Interfaces
{
    public interface IProjectWorkStatusQueryHandler
    {
        Task<List<ProjectWorkStatusEnum>> HandleAsync(FindProjectWorkStatusListQuery query);
    }
}
