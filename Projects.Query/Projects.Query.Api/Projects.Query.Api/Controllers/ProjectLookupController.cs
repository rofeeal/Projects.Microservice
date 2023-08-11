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
    public class ProjectLookupController : ControllerBase
    {
        private readonly ILogger<ProjectLookupController> _logger;
        private readonly IQueryDispatcher<ProjectEntity> _queryDispatcher;

        public ProjectLookupController(ILogger<ProjectLookupController> logger, IQueryDispatcher<ProjectEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProjectsAsync()
        {
            try
            {
                var projects = await _queryDispatcher.SendAsync(new FindAllProjectsQuery());
                return NormalResponse(projects);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all projects!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        [HttpGet("GetDeletedProjects")]
        public async Task<ActionResult> GetDeletedProjectsAsync()
        {
            try
            {
                var projects = await _queryDispatcher.SendAsync(new FindDeletedProjectsQuery());
                return NormalResponse(projects);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve deleted projects!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        [HttpGet("byId/{projectId}")]
        public async Task<ActionResult> GetByProjectIdAsync(Guid projectId)
        {
            try
            {
                var projects = await _queryDispatcher.SendAsync(new FindProjectByIdQuery { Id = projectId });
                return NormalResponse(projects);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to find project by ID!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        [HttpGet("byCode/{code}")]
        public async Task<ActionResult> GetProjectsByCodeAsync(string code)
        {
            try
            {
                var projects = await _queryDispatcher.SendAsync(new FindProjectByCodeQuery { Code = code });
                return NormalResponse(projects);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to find projects by code!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        private ActionResult NormalResponse(List<ProjectEntity> projects)
        {
            if (projects == null || !projects.Any())
                return NoContent();

            var count = projects.Count;
            return Ok(new ProjectLookupResponse
            {
                Results = projects,
                Message = $"Successfully returned {count} project{(count > 1 ? "s" : string.Empty)}!"
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
