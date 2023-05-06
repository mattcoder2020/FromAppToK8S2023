using Common.Messages;
using InventoryService.Models;
using System.Threading.Tasks;

namespace InventoryService.Service
{
    public interface IProductService
    {
        Task<Product[]> GetAll(ICorrelationContext context);
        Task<Product[]> GetByFiltration(QueryParams productparams, ICorrelationContext context);
        Task<bool> PutByProductId(int productid, int quantity, ICorrelationContext context);
    }
}