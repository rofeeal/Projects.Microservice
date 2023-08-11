using CQRS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Projects.Cmd.Api.Commands;
using Projects.Cmd.Domain.Aggregates;
using Projects.Common.DTOs;

namespace Projects.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RestoreReadDbProjectController : ControllerBase
    {
        private readonly ILogger<RestoreReadDbProjectController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public RestoreReadDbProjectController(ILogger<RestoreReadDbProjectController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> RestoreReadDbAsync()
        {
            try
            {
                await _commandDispatcher.SendAsync(new RestoreReadDbProjectCommand() { AggregateType = nameof(ProjectAggregate) });

                return StatusCode(StatusCodes.Status201Created, new BaseResponse
                {
                    Message = "Read database restore request completed successfully!"
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
                const string SAFE_ERROR_MESSAGE = "Error while processing request to restore read database!";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}