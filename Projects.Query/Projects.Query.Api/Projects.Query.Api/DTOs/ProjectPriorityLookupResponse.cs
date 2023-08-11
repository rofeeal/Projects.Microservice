using Projects.Common.DTOs;
using Projects.Query.Domain.Enum;

namespace Projects.Query.Api.DTOs
{
    public class ProjectPriorityLookupResponse : BaseResponse
    {
        public List<ProjectPriorityEnum> Results { get; set; }
    }
}