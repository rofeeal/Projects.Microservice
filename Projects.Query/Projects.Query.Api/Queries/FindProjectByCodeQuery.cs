using CQRS.Core.Queries;

namespace Projects.Query.Api.Queries
{
    public class FindProjectByCodeQuery : BaseQuery
    {
        public string Code { get; set; }
    }
}
