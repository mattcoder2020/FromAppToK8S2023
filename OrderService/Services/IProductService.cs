using OrderService.Models;
using OrderService.SQLiteDB;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public interface IProductService
    {
        OrderDBContext Dbcontext { get; }

        Task<Product> GetProductById(int id);
    }
}