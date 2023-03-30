using Common.DataAccess;
using OrderService.Models;
using OrderService.SQLiteDB;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class ProductService : IProductService
    {

        public ProductService(OrderDBContext dbcontext)
        {
            Dbcontext = dbcontext;
        }

        public OrderDBContext Dbcontext { get; }

        public async Task<Product> GetProductById(int id)
        {
            var repo = new GenericSqlServerRepository<Product, OrderDBContext>(Dbcontext);
            return await repo.FindByPrimaryKey(id);
        }
    }
}
