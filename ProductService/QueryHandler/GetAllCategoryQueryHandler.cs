using Common.DataAccess;
using Common.Handlers;
using Common.Messages;
using Common.Repo;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;
using ProductService.Query;
using ProductService.SQLiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.QueryHandler
{
    public class GetAllCategoryHandler : IQueryHandler<GetAllCategoryQuery, ProductCategory[]>
    {
        StoreDBContext _dbcontext;
        public GetAllCategoryHandler(StoreDBContext dbcontext)
        {
            //A repo implemenation can be defined as constructor param as DI approach 
            _dbcontext = dbcontext;
        }

        public async Task<ProductCategory[]> HandleAsync(GetAllCategoryQuery query, ICorrelationContext context)
        {
            var spec = new ProductIncludeCategory();
            var repository = new GenericSqlServerRepository<ProductCategory, StoreDBContext>(_dbcontext);
            IReadOnlyList<ProductCategory> productcategorys = await _dbcontext.ProductCategory.AsQueryable().ToListAsync();
            return productcategorys.ToArray();
            
        }

       
    }
}
