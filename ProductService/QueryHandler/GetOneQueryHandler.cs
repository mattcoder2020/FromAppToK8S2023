using Common.Handlers;
using Common.Messages;
using Common.Repo;
using ProductService.Models;
using ProductService.Query;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.QueryHandler
{
    public class GetOneQueryHandler : IQueryHandler<GetOneQuery, Product>
    {
        public GetOneQueryHandler()
        {
            //A repo implemenation can be defined as constructor param as DI approach 
        }
       
        public async Task<Product> HandleAsync(GetOneQuery query, ICorrelationContext context)
        {
             var list =  await DataStore<Product>.GetInstance().GetRecords(i => i.Id == query.Id);
              return list.FirstOrDefault();

        }


    }
}
