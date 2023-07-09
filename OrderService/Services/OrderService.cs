using Common.DataAccess;
using OrderService.Models;
using OrderService.SQLiteDB;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        public OrderService(OrderDBContext dbContext)
        {
            this.DbContext = dbContext;

        }
        private OrderDBContext DbContext { get; set; }

        public async Task<Order> GetOrderById(int id)
        {
            var repo = new GenericSqlServerRepository<Order, OrderDBContext>(DbContext);
            var spec = new OrderByIdSpec(id);
            return await repo.GetEntityBySpec(spec);

        }

        //write function for add order using unit of work pattern to persist its member list of order items to database in a transaction
        public async Task<Order> AddOrder(Order order)
        {
            var repo = new GenericSqlServerRepository<Order, OrderDBContext>(DbContext);
            var orderItemRepo = new GenericSqlServerRepository<OrderItem, OrderDBContext>(DbContext);
            var productRepo = new GenericSqlServerRepository<Product, OrderDBContext>(DbContext);

            foreach (var item in order.OrderItems)
            {
                var product = await productRepo.FindByPrimaryKey(item.Id);
                //if (product == null)
                //    throw new Exception("Product not found");
                item.Name = product.Name;
                item.Price = product.Price;
                item.OrderId = order.Id;
                item.ProductCategory = product.ProductCategoryId;
                
                orderItemRepo.AddModel(item);
            }
            //calculate sum of order.orderitems by aggregate price multiply by quantity in each order item
            order.Total = order.OrderItems.Sum(i=>i.Price*i.Quantity);
            repo.AddModel(order);
            await DbContext.SaveChangesAsync();
            DbContext.Dispose();
            return order;
        }



    }
}