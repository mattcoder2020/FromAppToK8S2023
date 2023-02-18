using Common.DataAccess;
using Common.Handlers;
using Common.Messages;
using ProductService.Models;
using ProductService.Query;
using ProductService.SQLiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.QueryHandler
{
    public class GetByFiltrationHandler : IQueryHandler<GetByFiltrationQuery, Product[]>
    {
        StoreDBContext _dbcontext;
        public GetByFiltrationHandler(StoreDBContext dbcontext)
        {
            //A repo implemenation can be defined as constructor param as DI approach 
            _dbcontext = dbcontext;
        }

        public async Task<Product[]> HandleAsync(GetByFiltrationQuery query, ICorrelationContext context)
        {
            var spec = new ProductByFiltrationSpec(query.QueryParams);
            var repository = new GenericSqlServerRepository<Product, StoreDBContext>(_dbcontext);
            IReadOnlyList<Product> products = await repository.GetEntityListBySpec(spec);
            return products.ToArray();

        }
    }
}
