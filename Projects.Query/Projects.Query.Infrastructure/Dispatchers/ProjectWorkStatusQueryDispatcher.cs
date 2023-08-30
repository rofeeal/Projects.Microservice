using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using Projects.Query.Domain.Entities;

namespace Projects.Query.Infrastructure.Dispatchers
{
    public class ProjectStatusQueryDispatcher : IQueryDispatcher<ProjectStatusEntity>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<ProjectStatusEntity>>>> _handlers = new();

        public void RegisterHandler<TQuery>(Func<TQuery, Task<List<ProjectStatusEntity>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
            {
                throw new IndexOutOfRangeException("You cannot register the same query handler twice!");
            }

            _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
        }

        public async Task<List<ProjectStatusEntity>> SendAsync(BaseQuery query)
        {
            if (_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<ProjectStatusEntity>>> handler))
            {
                return await handler(query);
            }

            throw new ArgumentNullException(nameof(handler), "No query handler was registered!");
        }
    }
}