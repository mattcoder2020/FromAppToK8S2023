using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Query
{
    public class GetAllQuery : IQuery<List<DemoModel>> 
    {
        public int Id { get; set; }
    }
}
