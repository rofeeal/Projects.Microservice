using CQRS.Core.Queries;

namespace Projects.Query.Api.Queries
{
    public class FindProjectByIdQuery : BaseQuery
    {
        public Guid Id { get; set; }
    }
}
