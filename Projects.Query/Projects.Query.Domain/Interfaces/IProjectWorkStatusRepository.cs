using Projects.Query.Domain.Enum;

namespace Projects.Query.Domain.Interfaces
{
    public interface IProjectWorkStatusRepository
    {
        Task<List<ProjectWorkStatusEnum>> ListAllAsync();
    }
}
