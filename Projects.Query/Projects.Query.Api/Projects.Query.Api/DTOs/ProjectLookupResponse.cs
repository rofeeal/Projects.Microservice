using Projects.Common.DTOs;
using Projects.Query.Domain.Entities;

namespace Projects.Query.Api.DTOs
{
    public class ProjectLookupResponse : BaseResponse
    {
        public List<ProjectEntity> Results { get; set; }
    }
}