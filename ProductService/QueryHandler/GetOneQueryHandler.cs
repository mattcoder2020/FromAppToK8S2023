using Common.DataAccess;
using Common.Handlers;
using Common.Messages;
using Common.Repo;
using ProductService.Models;
using ProductService.Query;
using ProductService.SQLiteDB;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.QueryHandler
{
    public class GetOneQueryHandler : IQueryHandler<GetOneQuery, Product>
    {
        StoreDBContext _dbcontext; 
        public GetOneQueryHandler(StoreDBContext dbcontext)
        {
            //A repo implemenation can be defined as constructor param as DI approach 
            _dbcontext = dbcontext;
        }
       
        public async Task<Product> HandleAsync(GetOneQuery query, ICorrelationContext context)
        {
            // var list =  await DataStore<Product>.GetInstance().GetRecords(i => i.Id == query.Id);
            var repository = new GenericSqlServerRepository<Product, StoreDBContext>(_dbcontext);
            var spec = new ProductByProductIdSpec(query.Id);
            return await repository.GetEntityBySpec(spec);
            // await repository.FindByPrimaryKey(query.Id);
        }
    }
}
