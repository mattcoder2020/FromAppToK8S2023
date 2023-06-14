using Common.Messages;
using System;

namespace InventoryService.Events
{
    [SubscriptionNamespace("InventoryService")]
    [MessageNamespace("ProductUpdated")]
    public class ProductUpdated : IEvent
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Category { get; set; }
        public Decimal Price { get; set; }
        public ICorrelationContext Context { get; set; }
    }
}
