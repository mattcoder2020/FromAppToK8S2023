using Common.DataAccess;
using Common.Handlers;
using Common.Messages;
using Common.Repo;
using ProductService.Commands;
using ProductService.Events;
using ProductService.Models;
using ProductService.SQLiteDB;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.CommandHandlers
{
    public class PutProductCommandHandler : ICommandHandler<PutProductCommand>
    {
        private readonly IMessageBrokerFactory _messageBrokerFactory;
        private StoreDBContext _dbcontext;
        // IBusPublisher _busPublisher;
        // IMetricRegistry appMetric;
        public PutProductCommandHandler(IMessageBrokerFactory messageBrokerFactory, StoreDBContext dbcontext
            //, IMetricRegistry AppMetric
            )
        {
            _messageBrokerFactory = messageBrokerFactory;
            _dbcontext = dbcontext;
            //appMetric = AppMetric;
        } 
        public async Task HandleAsync(PutProductCommand command, ICorrelationContext context)
        {
            var repository = new GenericSqlServerRepository<Product, StoreDBContext>(_dbcontext);
             repository.UpdateModel(command.Product);
            await _dbcontext.SaveChangesAsync();
            await _messageBrokerFactory.Publisher.PublishAsync<ProductUpdated>(new ProductUpdated { Id = command.Product.Id, Name = command.Product.Name, Price = command.Product.Price, Category= command.Product.ProductCategoryId }, context);
            
         }

        
    }
}
