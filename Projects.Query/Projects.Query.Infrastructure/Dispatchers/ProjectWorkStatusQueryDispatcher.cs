using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using Projects.Query.Domain.Enum;

namespace Projects.Query.Infrastructure.Dispatchers
{
    public class ProjectWorkStatusQueryDispatcher : IQueryDispatcher<ProjectWorkStatusEnum>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<ProjectWorkStatusEnum>>>> _handlers = new();

        public void RegisterHandler<TQuery>(Func<TQuery, Task<List<ProjectWorkStatusEnum>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
            {
                throw new IndexOutOfRangeException("You cannot register the same query handler twice!");
            }

            _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
        }

        public async Task<List<ProjectWorkStatusEnum>> SendAsync(BaseQuery query)
        {
            if (_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<ProjectWorkStatusEnum>>> handler))
            {
                return await handler(query);
            }

            throw new ArgumentNullException(nameof(handler), "No query handler was registered!");
        }
    }
}