using ProductService.Models;
using ProductService.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.QueryHandler
{
    public class GetOneQueryHandler : IQueryHandler<GetOneQuery, DemoModel>
    {
        public GetOneQueryHandler()
        {
            //A repo implemenation can be defined as constructor param as DI approach 
        }
        public DemoModel execute(GetOneQuery query)
        {
            return new DemoModel { Id = 0, Name = "Matt" };
        }
    }
}
