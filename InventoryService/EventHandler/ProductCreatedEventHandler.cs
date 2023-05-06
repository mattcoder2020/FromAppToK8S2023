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
    public class ProductCreatedEventHandler:IEventHandler<ProductCreated>
    {
        private readonly InventoryDBContext dbcontext;
        public ProductCreatedEventHandler(InventoryDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Task> HandleAsync(ProductCreated @event, ICorrelationContext context)
        {

            var repo = new GenericSqlServerRepository<Product, InventoryDBContext>(dbcontext);
            var obj = await repo.FindByPrimaryKey(@event.Id);
            if (obj == null)
            {
                repo.AddModel(new Product() { Id = @event.Id, Name = @event.Name, Price = @event.Price, ProductCategoryId = @event.Category });
                await dbcontext.SaveChangesAsync();
                return Task.CompletedTask;
            }
            throw new CustomizedException<ProductCreatedEventHandler>("HandleAsync-ProductExisted");
        }
    }
}
