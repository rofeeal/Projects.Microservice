using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using Projects.Query.Domain.Enum;

namespace Projects.Query.Infrastructure.Dispatchers
{
    public class ProjectPriorityQueryDispatcher : IQueryDispatcher<ProjectPriorityEnum>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<ProjectPriorityEnum>>>> _handlers = new();

        public void RegisterHandler<TQuery>(Func<TQuery, Task<List<ProjectPriorityEnum>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
            {
                throw new IndexOutOfRangeException("You cannot register the same query handler twice!");
            }

            _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
        }

        public async Task<List<ProjectPriorityEnum>> SendAsync(BaseQuery query)
        {
            if (_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<ProjectPriorityEnum>>> handler))
            {
                return await handler(query);
            }

            throw new ArgumentNullException(nameof(handler), "No query handler was registered!");
        }
    }
}