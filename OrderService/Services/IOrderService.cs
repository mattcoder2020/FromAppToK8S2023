using OrderService.Models;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}