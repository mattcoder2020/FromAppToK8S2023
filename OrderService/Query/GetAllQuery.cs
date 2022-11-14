using DynamicDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDI.Query
{
    public class GetAllQuery : IQuery<List<DemoModel>> 
    {
        public int Id { get; set; }
    }
}
