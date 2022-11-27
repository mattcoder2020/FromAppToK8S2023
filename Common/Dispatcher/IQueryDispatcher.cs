using Common.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dispatcher
{
    public interface IQueryDispatcher
    {
        Task<TResult> Query<TQuery,TResult>(TQuery query) where TQuery:IQuery;
    }
}
