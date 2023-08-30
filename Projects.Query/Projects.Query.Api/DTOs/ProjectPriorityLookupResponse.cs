using Projects.Common.DTOs;
using Projects.Query.Domain.Entities;

namespace Projects.Query.Api.DTOs
{
    public class ProjectPriorityLookupResponse : BaseResponse
    {
        public List<ProjectPriorityEntity> Results { get; set; }
    }
}