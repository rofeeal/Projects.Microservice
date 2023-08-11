using CQRS.Core.Commands;

namespace Projects.Cmd.Api.Commands
{
    public class RestoreReadDbProjectCommand : BaseCommand
    {
        public string AggregateType { get; set; }
    }
}
