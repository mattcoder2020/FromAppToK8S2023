using Common.Messages;
using System;

namespace InventoryService.Events
{
    [SubscriptionNamespace("Matt-InventoryUpdated")]
    [MessageNamespace("Matt-Inventory")]
    public class InventoryUpdated : IEvent
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ICorrelationContext Context { get; set; }
    }
}
