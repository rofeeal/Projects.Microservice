using Projects.Common.Events;
using Projects.Query.Domain.Entities;
using Projects.Query.Domain.Interfaces;
using Projects.Query.Infrastructure.Interfaces;

namespace Projects.Query.Infrastructure.Handlers
{
    public class AllEventHandler : IEventHandler
    {
        private readonly IProjectRepository _projectRepository;
        private readonly static string PRECODE = "PRO-";

        public AllEventHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task On(ProjectCreatedEvent @event)
        {
            var project = new ProjectEntity
            {
                Id = @event.Id,
                Code = PRECODE + @event.Code.ToString(),
                ParentId = @event.ParentId,
                RootId = await _projectRepository.GetRootIDAsync(@event.ParentId),
                IsParent = @event.IsParent,
                Name = @event.Name,
                PriorityId = @event.PriorityId,
                StatusId = @event.StatusId,
                ClientId = @event.ClientId,
                Price = @event.Price,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                Description = @event.Description,
                Image = @event.Image,
                Active = @event.Active,
        };

            await _projectRepository.CreateAsync(project);
        }

        public async Task On(ProjectEditedEvent @event)
        {
            var project = await _projectRepository.GetByIdAsync(@event.Id);

            if (project == null) return;

            project.Id = @event.Id;
            project.Code = PRECODE + @event.Code.ToString();
            project.ParentId = @event.ParentId;
            project.RootId = await _projectRepository.GetRootIDAsync(@event.ParentId);
            project.IsParent = @event.IsParent;
            project.Name = @event.Name;
            project.PriorityId = @event.PriorityId;
            project.StatusId = @event.StatusId;
            project.ClientId = @event.ClientId;
            project.Price = @event.Price;
            project.StartDate = @event.StartDate;
            project.EndDate = @event.EndDate;
            project.Description = @event.Description;
            project.Image = @event.Image;
            project.Active = @event.Active;

            await _projectRepository.UpdateAsync(project);
        }

        public async Task On(ProjectDeletedEvent @event)
        {
            var project = await _projectRepository.GetByIdAsync(@event.Id);

            if (project == null) return;

            await _projectRepository.DeleteAsync(project.Id);
        }

        public async Task On(ProjectPermanentlyDeletedEvent @event)
        {
            var project = await _projectRepository.GetByIdAsync(@event.Id);

            if (project == null) return;

            await _projectRepository.DeletePermanentlyAsync(project.Id);
        }
    }
}
