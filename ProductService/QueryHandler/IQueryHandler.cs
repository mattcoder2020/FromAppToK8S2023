using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.QueryHandler
{
    interface IQueryHandler<IQuery, TResult>
    {
        TResult execute(IQuery query);
    }
}
