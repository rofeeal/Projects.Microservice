using Projects.Common.DTOs;

namespace Projects.Cmd.Api.DTOs
{
    public class NewProjectResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}