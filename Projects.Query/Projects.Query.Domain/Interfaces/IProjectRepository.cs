
using Projects.Query.Domain.Entities;

namespace Projects.Query.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task CreateAsync(ProjectEntity project);
        Task UpdateAsync(ProjectEntity project);
        Task DeleteAsync(Guid projectId);
        Task DeletePermanentlyAsync(Guid projectId);
        Task<ProjectEntity> GetByIdAsync(Guid projectId);
        Task<ProjectEntity> GetByCodeAsync(string projectCode);
        Task<List<ProjectEntity>> ListAllAsync();
        Task<List<ProjectEntity>> ListDeletedAsync();
        Task<Guid> GetRootIDAsync(Guid parentId);
    }
}
