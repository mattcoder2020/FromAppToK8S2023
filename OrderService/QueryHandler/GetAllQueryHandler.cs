using DynamicDI.Models;
using DynamicDI.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDI.QueryHandler
{
    public class GetAllQueryHandler : IQueryHandler<GetAllQuery, List<DemoModel>>
    {
        public GetAllQueryHandler()
        {
            //Here you can put repository implmentation at the constructor
        }
        public List<DemoModel> execute(GetAllQuery query)
        {
            //repo.getall
            return new List<DemoModel>
            {
                new DemoModel{Id=0, Name="Matt"},
                new DemoModel{Id=1, Name="Yang"}
             };
        }
    }
}
