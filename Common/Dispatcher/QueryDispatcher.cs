using Autofac;
using Common.Handlers;
using Common.Messages;
using Common.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dispatcher
{
    public class QueryDispatcher:IQueryDispatcher
    {
        private IComponentContext _context;
        public QueryDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            //implementation new
            var type = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic queryHandler = _context.Resolve(type);
            //await CommandHandler.HandleAsync(command, CorrelationContext.Empty);
            return queryHandler.HandleAsync((dynamic)query, CorrelationContext.Empty);
        }
    }
}
