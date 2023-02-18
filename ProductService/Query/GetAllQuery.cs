using Common.Dispatcher;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Query
{
    public class GetAllQuery : IQuery
    {
    }

    public class GetByFiltrationQuery : IQuery
    {
        public GetByFiltrationQuery(QueryParams QueryParams)
        {
            this.QueryParams = QueryParams;

        }
        public QueryParams QueryParams { get; set; }
    }

    public class GetAllCategoryQuery : IQuery
    {
    }
}
