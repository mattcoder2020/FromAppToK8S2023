using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicDI.Models;

namespace DynamicDI.Query
{
    public class GetOneQuery: IQuery<DemoModel>
    {
        public int Id { get; set; }
    }
}
