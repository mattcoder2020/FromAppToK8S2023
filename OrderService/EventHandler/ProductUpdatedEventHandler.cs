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
    public class InventoryUpdatedEventHandler : IEventHandler<InventoryUpdated>
    {
        private readonly OrderDBContext _dbcontext;
        public InventoryUpdatedEventHandler(OrderDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public async Task<Task> HandleAsync(InventoryUpdated @event, ICorrelationContext context)
        {

            var repo = new GenericSqlServerRepository<Product, OrderDBContext>(_dbcontext);
            var obj = await repo.FindByPrimaryKey(@event.ProductId);
            if (obj != null)
            {
                obj.Quantity = @event.Quantity;
                repo.UpdateModel(obj);
                await _dbcontext.SaveChangesAsync();
                return Task.CompletedTask;
            }
            throw new CustomizedException<InventoryUpdatedEventHandler>("HandleAsync-InventoryUpdated");
        }
    }
}
