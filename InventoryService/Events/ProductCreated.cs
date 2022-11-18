using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Events
{
    [SubscriptionNamespace("Matt-Product")]
    [MessageNamespace("Matt-Product")]
    public class ProductCreated : IEvent
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ICorrelationContext Context { get; set; }
    }
}
