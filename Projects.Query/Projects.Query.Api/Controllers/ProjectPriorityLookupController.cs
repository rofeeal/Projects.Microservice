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
    public class ProjectPriorityLookupController : ControllerBase
    {
        private readonly ILogger<ProjectPriorityLookupController> _logger;
        private readonly IQueryDispatcher<ProjectPriorityEntity> _queryDispatcher;

        public ProjectPriorityLookupController(ILogger<ProjectPriorityLookupController> logger, IQueryDispatcher<ProjectPriorityEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetProjectPriorityListAsync()
        {
            try
            {
                var projectPriority = await _queryDispatcher.SendAsync(new FindProjectPriorityListQuery());
                return NormalResponse(projectPriority);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all project types!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        private ActionResult NormalResponse(List<ProjectPriorityEntity> projectTypes)
        {
            if (projectTypes == null || !projectTypes.Any())
                return NoContent();

            var count = projectTypes.Count;
            return Ok(new ProjectPriorityLookupResponse
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
