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
    public class ProductCreatedEventHandler : IEventHandler<ProductCreated>
    {
        private readonly OrderDBContext dbcontext;
        public ProductCreatedEventHandler(OrderDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Task> HandleAsync(ProductCreated @event, ICorrelationContext context)
        {
            
                var repo = new GenericSqlServerRepository<Product, OrderDBContext>(dbcontext);
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

    //public class ProductCreatedEventHandler : IEventHandler<ProductCreated, OrderDBContext>
    //{
    //    public async Task<Task> HandleAsync(ProductCreated @event, OrderDBContext dbcontext, ICorrelationContext context)
    //    {
    //        var repo = new GenericSqlServerRepository<Product, OrderDBContext>(dbcontext);
    //        var obj = await repo.FindByPrimaryKey(@event.Id);
    //        if (obj == null)
    //            return repo.AddModel(new Product() { Id = @event.Id, Name = @event.Name, Price = @event.Price, ProductCategoryId = @event.ProductCategoryId });
    //        throw new CustomizedException<ProductCreatedEventHandler>("HandleAsync-ProductExisted");

    //    }

    //}
}
