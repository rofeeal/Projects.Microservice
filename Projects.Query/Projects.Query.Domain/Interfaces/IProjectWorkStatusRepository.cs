using Projects.Query.Domain.Entities;

namespace Projects.Query.Domain.Interfaces
{
    public interface IProjectStatusRepository
    {
        Task<List<ProjectStatusEntity>> ListAllAsync();
    }
}
