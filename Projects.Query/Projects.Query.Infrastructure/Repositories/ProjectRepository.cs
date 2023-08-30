using Microsoft.EntityFrameworkCore;
using Projects.Query.Domain.Entities;
using Projects.Query.Domain.Interfaces;
using Projects.Query.Infrastructure.DataAccess;

namespace Projects.Query.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public ProjectRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(ProjectEntity project)
        {
            try
            {
                using (DatabaseContext context = _contextFactory.CreateDbContext())
                {
                    project.SetCreatedInfo("user id");

                    context.Projects.Add(project);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while creating the project: {ex.Message}");
            }
        }

        public async Task DeleteAsync(Guid projectId)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var project = await GetByIdAsync(projectId);

                    if (project == null)
                    {
                        throw new Exception("Project not found.");
                    }

                    // Update sub-project delete
                    await UpdateSubProjectDeletedAsync(project.Id, true);

                    // Mark the project as deleted and update deletion metadata
                    project.SetDeletedInfo("User Id");

                    context.Projects.Update(project);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Failed to mark the project as deleted. Please try again later.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while marking the project as deleted.", ex);
                }
            }
        }

        private async Task UpdateSubProjectDeletedAsync(Guid projectId, bool newDeletedState)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                var subProjects = await context.Projects
                    .Where(p => p.RootId == projectId)
                    .ToListAsync();

                foreach (var subProject in subProjects)
                {
                    subProject.IsDeleted = newDeletedState;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task DeletePermanentlyAsync(Guid projectId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();

            try
            {
                var project = await GetByIdAsync(projectId);

                await DeleteSubProjectsPermanentlyAsync(projectId);

                if (project == null)
                {
                    throw new Exception("Project not found.");
                }

                context.Projects.Remove(project);
                _ = await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to delete project. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the project.", ex);
            }
        }
        
        private async Task DeleteSubProjectsPermanentlyAsync(Guid parentId)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                var subProjects = await context.Projects
                    .Where(p => p.ParentId == parentId)
                    .ToListAsync();

                foreach (var subProject in subProjects)
                {
                    context.Projects.Remove(subProject);
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<ProjectEntity> GetByCodeAsync(string projectCode)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Projects
                    .Where(x => x.Code == projectCode)
                    .FirstOrDefaultAsync();
        }

        public async Task<ProjectEntity> GetByIdAsync(Guid projectId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);
        }

        public async Task<Guid> GetRootIDAsync(Guid parentId)
        {
            try
            {
                using DatabaseContext context = _contextFactory.CreateDbContext();

                var current = await context.Projects
                    .Where(p => p.Id == parentId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (current != null)
                {
                    if (current.ParentId != Guid.Empty)
                    {
                        return await GetRootIDAsync(current.ParentId);
                    }
                    else
                    {
                        return current.Id;
                    }
                }
                else
                {
                    // Return a default Guid or throw an exception, depending on your requirement
                    return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, such as logging or rethrowing
                // For now, let's rethrow the exception
                throw;
            }
        }

        public async Task<List<ProjectEntity>> ListAllAsync()
        {
            try
            {
                using (DatabaseContext context = _contextFactory.CreateDbContext())
                {
                    return await context.Projects
                        .Where(p => !p.IsDeleted) 
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the undeleted project list.", ex);
            }
        }

        public async Task<List<ProjectEntity>> ListDeletedAsync()
        {
            try
            {
                using (DatabaseContext context = _contextFactory.CreateDbContext())
                {
                    return await context.Projects
                        .Where(p => p.IsDeleted) 
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the deleted project list.", ex);
            }
        }

        public async Task UpdateAsync(ProjectEntity project)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();

            try
            {
                var userId = "user id";
                await UpdateSubProjectAsync(project.Id, project.RootId, project.Active, userId);

                project.SetModifiedInfo(userId);
                context.Projects.Update(project);

                _ = await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to update project. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the project.", ex);
            }
        }

        private async Task UpdateSubProjectAsync(Guid projectId, Guid newRootId, bool newState, string userId)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                var subProjects = await context.Projects
                    .Where(p => p.ParentId == projectId)
                    .ToListAsync();

                foreach (var subProject in subProjects)
                {
                    subProject.Active = newState;
                    subProject.DeletedBy = userId;
                    subProject.RootId = newRootId;
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
