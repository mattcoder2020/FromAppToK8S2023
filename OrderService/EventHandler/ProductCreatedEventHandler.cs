using Common.Exceptions;
using Common.Handlers;
using Common.Messages;
using Common.RabbitMQ;
using Common.Repo;
using OrderService.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.EventHandler
{
    public class ProductCreatedEventHandler : IEventHandler<ProductCreated>
    {
        public Task HandleAsync(ProductCreated @event, ICorrelationContext context)
        {
            var dStore = DataStore<ProductCreated>.GetInstance();
            if (dStore.GetRecords(i => i.Id == @event.Id).Count() > 0)
                throw new CustomizedException<ProductCreatedEventHandler>("HandleAsync-ProductExisted");
            dStore.AddRecord(@event);
            return Task.CompletedTask;
        }
    }
}
