using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dispatcher;
using ProductService.Models;

namespace ProductService.Query
{
    public class GetOneQuery: IQuery
    {
        public int Id { get; set; }
    }
}
