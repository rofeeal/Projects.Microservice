using CQRS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Projects.Cmd.Api.Commands;
using Projects.Cmd.Api.DTOs;
using Projects.Common.DTOs;

namespace Projects.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewProjectController : ControllerBase
    {
        private readonly ILogger<NewProjectController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public NewProjectController(ILogger<NewProjectController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> NewProjectAsync(NewProjectCommand command)
        {
            var id = Guid.NewGuid();
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status201Created, new NewProjectResponse
                {
                    Id = id,
                    Message = "New project creation request completed successfully!"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Project made a bad request!");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new project!";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new NewProjectResponse
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}