using Common.Exceptions;
using Common.Handlers;
using Common.Messages;
using Common.Repo;
using InventoryService.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.EventHandler
{
    public class ProductCreatedEventHandler:IEventHandler<ProductCreated>
    {
        public Task HandleAsync (ProductCreated @event, ICorrelationContext context)
        {
            var datastore = DataStore<ProductCreated>.GetInstance();
            if (datastore.GetRecords(i => i.Name == @event.Name).Count() > 0)
                throw new CustomizedException<ProductCreatedEventHandler>("HandleAsync-ProductExisted");

            datastore.AddRecord(@event);
            return Task.CompletedTask;
        }
    }
}
