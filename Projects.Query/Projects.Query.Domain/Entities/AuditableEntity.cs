
namespace Projects.Query.Domain.Entities
{
    public class AuditableEntity
    {
        public DateTimeOffset? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

        public void SetCreatedInfo(string createdBy)
        {
            CreatedOn = DateTimeOffset.UtcNow;
            CreatedBy = createdBy;
        }

        public void SetModifiedInfo(string modifiedBy)
        {
            ModifiedOn = DateTimeOffset.UtcNow;
            ModifiedBy = modifiedBy;
        }

        public void SetDeletedInfo(string deletedBy)
        {
            DeletedOn = DateTimeOffset.UtcNow;
            DeletedBy = deletedBy;
            IsDeleted = true;
        }
    }
}
