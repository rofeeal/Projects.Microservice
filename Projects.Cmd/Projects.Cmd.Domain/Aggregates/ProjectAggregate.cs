using CQRS.Core.Domain;
using Projects.Common.Events;

namespace Projects.Cmd.Domain.Aggregates
{
    public class ProjectAggregate : AggregateRoot
    {
        public int _code;
        public Guid _parentId;
        public bool _isParent;
        public string _name;
        public Guid _priorityId;
        public Guid _statusId;
        public Guid? _clientId;
        public float? _price;
        public DateTime? _startDate;
        public DateTime? _endDate;
        public string? _description;
        public string? _image;
        public bool _active;


        public ProjectAggregate()
        {
        }

        public ProjectAggregate(Guid id, int code, Guid parentId, bool isParent, string name, Guid priorityId, Guid statusId, Guid? clientId, float? price, DateTime? startDate, DateTime? endDate, string? description, string? image, bool active)
        {
            RaiseEvent(new ProjectCreatedEvent
            {
                Id = id,
                Code = code,
                ParentId = parentId,
                IsParent = isParent,
                Name = name,
                PriorityId = priorityId,
                StatusId = statusId,
                ClientId = clientId,
                Price = price,
                StartDate = startDate,
                EndDate = endDate,
                Description = description,
                Image = image,
                Active = active
            });
            
        }

        public void Apply(ProjectCreatedEvent @event)
        {
            _id = @event.Id;
            _code = @event.Code;
            _parentId = @event.ParentId;
            _isParent = @event.IsParent;
            _name = @event.Name;
            _priorityId = @event.PriorityId;
            _statusId = @event.StatusId;
            _clientId = @event.ClientId;
            _price = @event.Price;
            _startDate = @event.StartDate;
            _endDate = @event.EndDate;
            _description = @event.Description;
            _image = @event.Image;
            _active = @event.Active;
        }

        public void EditProjectAggregate(Guid id, int code, Guid parentId, bool isParent, string name, Guid priorityId, Guid statusId, Guid? clientId, float? price, DateTime? startDate, DateTime? endDate, string? description, string? image, bool active)
        {
            RaiseEvent(new ProjectEditedEvent
            {
                Id = id,
                Code = code,
                ParentId = parentId,
                IsParent = isParent,
                Name = name,
                PriorityId = priorityId,
                StatusId = statusId,
                ClientId = clientId,
                Price = price,
                StartDate = startDate,
                EndDate = endDate,
                Description = description,
                Image = image,
                Active = active,
            });
        }

        public void Apply(ProjectEditedEvent @event)
        {
            _id = @event.Id;
            _code = @event.Code;
            _parentId = @event.ParentId;
            _isParent = @event.IsParent;
            _name = @event.Name;
            _priorityId = @event.PriorityId;
            _statusId = @event.StatusId;
            _clientId = @event.ClientId;
            _price = @event.Price;
            _startDate = @event.StartDate;
            _endDate = @event.EndDate;
            _description = @event.Description;
            _image = @event.Image;
            _active = @event.Active;
        }

        public void DeleteProject(Guid id)
        {
            RaiseEvent(new ProjectDeletedEvent
            {
                Id = id
            });
        }

        public void Apply(ProjectDeletedEvent @event)
        {
            _id = @event.Id;
        }

        public void DeleteProjectPermanently(Guid id)
        {
            RaiseEvent(new ProjectPermanentlyDeletedEvent
            {
                Id = id
            });
        }

        public void Apply(ProjectPermanentlyDeletedEvent @event)
        {
            _id = @event.Id;
        }

    }
}
