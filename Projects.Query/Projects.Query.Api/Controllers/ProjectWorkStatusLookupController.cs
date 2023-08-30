using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Projects.Common.DTOs;
using Projects.Query.Api.DTOs;
using Projects.Query.Api.Queries;
using Projects.Query.Domain.Entities;

namespace Projects.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectStatusLookupController : ControllerBase
    {
        private readonly ILogger<ProjectStatusLookupController> _logger;
        private readonly IQueryDispatcher<ProjectStatusEntity> _queryDispatcher;

        public ProjectStatusLookupController(ILogger<ProjectStatusLookupController> logger, IQueryDispatcher<ProjectStatusEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetProjectStatusListAsync()
        {
            try
            {
                var projectPriority = await _queryDispatcher.SendAsync(new FindProjectStatusListQuery());
                return NormalResponse(projectPriority);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all project types!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        private ActionResult NormalResponse(List<ProjectStatusEntity> projectTypes)
        {
            if (projectTypes == null || !projectTypes.Any())
                return NoContent();

            var count = projectTypes.Count;
            return Ok(new ProjectStatusLookupResponse
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
