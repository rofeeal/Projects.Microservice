using Projects.Common.DTOs;
using Projects.Query.Domain.Enum;

namespace Projects.Query.Api.DTOs
{
    public class ProjectWorkStatusLookupResponse : BaseResponse
    {
        public List<ProjectWorkStatusEnum> Results { get; set; }
    }
}