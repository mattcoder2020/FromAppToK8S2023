using Common.Handlers;
using Common.Messages;
using Common.Repo;
using ProductService.Models;
using ProductService.Query;
using System.Linq;


namespace ProductService.QueryHandler
{
    public class GetOneQueryHandler : IQueryHandler<GetOneQuery, Product>
    {
        public GetOneQueryHandler()
        {
            //A repo implemenation can be defined as constructor param as DI approach 
        }
       
        public Product HandleAsync(GetOneQuery query, ICorrelationContext context)
        {
            return DataStore<Product>.GetInstance().GetRecords(i => i.Id == query.Id).FirstOrDefault();
        }


    }
}
