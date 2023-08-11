using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Projects.Common.DTOs;
using Projects.Common.Enum;
using Projects.Query.Api.DTOs;
using Projects.Query.Api.Queries;
using Projects.Query.Domain.Enum;

namespace Projects.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectWorkStatusLookupController : ControllerBase
    {
        private readonly ILogger<ProjectWorkStatusLookupController> _logger;
        private readonly IQueryDispatcher<ProjectWorkStatusEnum> _queryDispatcher;

        public ProjectWorkStatusLookupController(ILogger<ProjectWorkStatusLookupController> logger, IQueryDispatcher<ProjectWorkStatusEnum> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetProjectWorkStatusListAsync()
        {
            try
            {
                var projectPriority = await _queryDispatcher.SendAsync(new FindProjectWorkStatusListQuery());
                return NormalResponse(projectPriority);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all project types!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        private ActionResult NormalResponse(List<ProjectWorkStatusEnum> projectTypes)
        {
            if (projectTypes == null || !projectTypes.Any())
                return NoContent();

            var count = projectTypes.Count;
            return Ok(new ProjectWorkStatusLookupResponse
            {
                Results = projectTypes,
                Message = $"Successfully returned {count} project priority{(count > 1 ? "s" : string.Empty)}!"
            });
        }

        private ActionResult ErrorResponse(Exception ex, string safeErrorMessage)
        {
            _logger.LogError(ex, safeErrorMessage);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = safeErrorMessage
            });
        }
    }
}
