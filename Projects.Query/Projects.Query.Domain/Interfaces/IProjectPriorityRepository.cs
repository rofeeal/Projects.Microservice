using Projects.Query.Domain.Enum;

namespace Projects.Query.Domain.Interfaces
{
    public interface IProjectPriorityRepository
    {
        Task<List<ProjectPriorityEnum>> ListAllAsync();
    }
}
