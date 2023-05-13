using Common.DataAccess;
using Common.Exceptions;
using Common.Handlers;
using Common.Messages;
using OrderService.Events;
using OrderService.Models;
using OrderService.SQLiteDB;
using System.Threading.Tasks;

namespace OrderService.EventHandler
{
    public class ProductUpdatedEventHandler : IEventHandler<ProductUpdated>
    {
        private readonly OrderDBContext dbcontext;
        public ProductUpdatedEventHandler(OrderDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Task> HandleAsync(ProductUpdated @event, ICorrelationContext context)
        {

            var repo = new GenericSqlServerRepository<Product, OrderDBContext>(dbcontext);
            var obj = await repo.FindByPrimaryKey(@event.Id);
            if (obj != null)
            {
                obj.Id = @event.Id;
                obj.Name = @event.Name;
                obj.Price = @event.Price;
                obj.ProductCategoryId = @event.Category;

                repo.UpdateModel(obj);
                await dbcontext.SaveChangesAsync();
                return Task.CompletedTask;
            }
            throw new CustomizedException<ProductUpdatedEventHandler>("HandleAsync-ProductNotExisted");
        }
    }
}
