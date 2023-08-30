using Projects.Query.Domain.Entities;

namespace Projects.Query.Domain.Interfaces
{
    public interface IProjectPriorityRepository
    {
        Task<List<ProjectPriorityEntity>> ListAllAsync();
    }
}
