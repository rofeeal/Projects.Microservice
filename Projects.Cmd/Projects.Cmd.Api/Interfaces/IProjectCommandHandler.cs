using Projects.Cmd.Api.Commands;

namespace Projects.Cmd.Api.Interfaces
{
    public interface IProjectCommandHandler
    {
        Task HandleAsync(NewProjectCommand command);
        Task HandleAsync(EditProjectCommand command);
        Task HandleAsync(DeleteProjectCommand command);
        Task HandleAsync(DeleteProjectPermanentlyCommand command);
        Task HandleAsync(RestoreReadDbProjectCommand command);
    }
}
