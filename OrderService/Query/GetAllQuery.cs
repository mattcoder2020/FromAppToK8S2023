using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Query
{
    public class GetAllQuery : IQuery<List<DemoModel>> 
    {
        public int Id { get; set; }
    }
}
