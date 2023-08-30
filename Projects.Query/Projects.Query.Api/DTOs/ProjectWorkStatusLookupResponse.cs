using Projects.Common.DTOs;
using Projects.Query.Domain.Entities;

namespace Projects.Query.Api.DTOs
{
    public class ProjectStatusLookupResponse : BaseResponse
    {
        public List<ProjectStatusEntity> Results { get; set; }
    }
}