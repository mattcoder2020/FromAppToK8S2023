using Common.DataAccess;
using Common.Exceptions;
using Common.Handlers;
using Common.Messages;
using InventoryService.Events;
using InventoryService.Models;
using InventoryService.NpgSqlDB;
using System.Threading.Tasks;

namespace InventoryService.EventHandler
{
    public class ProductUpdatedEventHandler : IEventHandler<ProductUpdated>
    {
        private readonly InventoryDBContext dbcontext;
        public ProductUpdatedEventHandler(InventoryDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Task> HandleAsync(ProductUpdated @event, ICorrelationContext context)
        {

            var repo = new GenericSqlServerRepository<Product, InventoryDBContext>(dbcontext);
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
