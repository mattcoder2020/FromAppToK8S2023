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
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IMessageBrokerFactory _messageBrokerFactory;
        private StoreDBContext _dbcontext;
        // IBusPublisher _busPublisher;
        // IMetricRegistry appMetric;
        public DeleteProductCommandHandler(IMessageBrokerFactory messageBrokerFactory, StoreDBContext dbcontext
            //, IMetricRegistry AppMetric
            )
        {
            _messageBrokerFactory = messageBrokerFactory;
            _dbcontext = dbcontext;
            //appMetric = AppMetric;
        } 
        public Task HandleAsync(DeleteProductCommand command, ICorrelationContext context)
        {
            var repository = new GenericSqlServerRepository<Product, StoreDBContext>(_dbcontext);
            repository.DeleteModel(new Product(command.Id, string.Empty, 0, 0));
            return Task.CompletedTask;
            //await _messageBrokerFactory.Publisher.PublishAsync<ProductCreated>(new ProductCreated { Id = command.Id, Name = command.Name }, context);
            
         }

        
    }
}
