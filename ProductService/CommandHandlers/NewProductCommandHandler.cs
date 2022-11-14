using Common.Handlers;
using Common.Messages;
using Common.Repo;
using ProductService.Commands;
using ProductService.Events;
//using ProductService.Metrics;
using ProductService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.CommandHandlers
{
    public class NewProductCommandHandler : ICommandHandler<NewProductCommand>
    {
        private readonly IMessageBrokerFactory _messageBrokerFactory;

        // IBusPublisher _busPublisher;
        // IMetricRegistry appMetric;
        public NewProductCommandHandler(IMessageBrokerFactory messageBrokerFactory
            //, IMetricRegistry AppMetric
            )
        {
            _messageBrokerFactory = messageBrokerFactory;
            //appMetric = AppMetric;
        } 
        public async Task  HandleAsync(NewProductCommand command, ICorrelationContext context)
        {
           var enumerator = DataStore<Product>.GetInstance().GetRecords(i=>i.Name == command.Name) ;
            if (enumerator.Count() == 0 )
            {
                DataStore<Product>.GetInstance().AddRecord
                    (new Product(command.Id, command.Name, command.Category, command.Price));
                //Send product created event bus msg
                await _messageBrokerFactory.Publisher.PublishAsync<ProductCreated>( new ProductCreated { Id = command.Id, Name = command.Name }, context);
            }
            else
            {
                await _messageBrokerFactory.Publisher.PublishAsync<RejectedEvent>(new RejectedEvent (command.Name + " already exists", command.Id.ToString()), context);
            }
            //appMetric.IncrementPostProductCount(); // 


        }
    }
}
