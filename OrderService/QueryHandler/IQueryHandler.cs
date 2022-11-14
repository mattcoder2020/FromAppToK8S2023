using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDI.QueryHandler
{
    interface IQueryHandler<IQuery, TResult>
    {
        TResult execute(IQuery query);
    }
}
