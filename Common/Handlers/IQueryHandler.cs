using Common.Messages;
using Common.RabbitMQ;
using Common.Dispatcher;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Handlers
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        TResult HandleAsync(TQuery @query, ICorrelationContext context);
    }
}
