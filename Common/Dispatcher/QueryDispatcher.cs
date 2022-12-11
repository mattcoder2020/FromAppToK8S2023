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
            var queryHandler = _context.Resolve<IQueryHandler<TQuery, TResult>>();
            //var type = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            //dynamic queryHandler = _context.Resolve(type);
            
            return queryHandler.HandleAsync(query, CorrelationContext.Empty);
        }
    }
}
