using Common.DataAccess;
using Common.Handlers;
using Common.Messages;
using Common.Repo;
using ProductService.Commands;
using ProductService.Events;
using ProductService.Models;
using ProductService.SQLiteDB;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.CommandHandlers
{
    public class NewProductCommandHandler : ICommandHandler<NewProductCommand>
    {
        private readonly IMessageBrokerFactory _messageBrokerFactory;
        private StoreDBContext _dbcontext;
        // IBusPublisher _busPublisher;
        // IMetricRegistry appMetric;
        public NewProductCommandHandler(IMessageBrokerFactory messageBrokerFactory, StoreDBContext dbcontext
            //, IMetricRegistry AppMetric
            )
        {
            _messageBrokerFactory = messageBrokerFactory;
            _dbcontext = dbcontext;
            //appMetric = AppMetric;
        } 
        public async Task  HandleAsync(NewProductCommand command, ICorrelationContext context)
        {
            var repository = new GenericSqlServerRepository<Product, StoreDBContext>(_dbcontext);
            var spec = new ProductByNameSpec(command.Name);
            Product existedwithName = null; 
            try
            {
                existedwithName = await repository.GetEntityBySpec(spec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + "  "+ ex.StackTrace); 
            }
            if (existedwithName == null)
            { 
            await repository.AddModel(new Product(command.Id, command.Name, command.CategoryId, command.Price));
            await _messageBrokerFactory.Publisher.PublishAsync<ProductCreated>(new ProductCreated { Id = command.Id, Name = command.Name }, context);
            }
            else
            {
                await _messageBrokerFactory.Publisher.PublishAsync<RejectedEvent>(new RejectedEvent(command.Name + " already exists", command.Id.ToString()), context);
            }
        }

        public async Task HandleAsync1(NewProductCommand command, ICorrelationContext context)
        {
            var enumerator = await DataStore<Product>.GetInstance().GetRecords(i => i.Name == command.Name);
            if (enumerator.Count() == 0)
            {
                DataStore<Product>.GetInstance().AddRecord
                    (new Product(command.Id, command.Name, command.CategoryId, command.Price));
                //Send product created event bus msg
                await _messageBrokerFactory.Publisher.PublishAsync<ProductCreated>(new ProductCreated { Id = command.Id, Name = command.Name }, context);
            }
            else
            {
                await _messageBrokerFactory.Publisher.PublishAsync<RejectedEvent>(new RejectedEvent(command.Name + " already exists", command.Id.ToString()), context);
            }
            //appMetric.IncrementPostProductCount(); // 
        }
    }
}
