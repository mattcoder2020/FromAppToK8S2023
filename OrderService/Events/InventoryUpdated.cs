using Common.Messages;
using System;

namespace OrderService.Events
{
    //[SubscriptionNamespace("OrderService")]
    [MessageNamespace("InventoryUpdated")]
    public class InventoryUpdated : IEvent
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ICorrelationContext Context { get; set; }
    }
}
