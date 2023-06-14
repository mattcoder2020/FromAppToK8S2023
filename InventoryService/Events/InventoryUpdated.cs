﻿using Common.Messages;
using System;

namespace InventoryService.Events
{
    [SubscriptionNamespace("InventoryService")]
    [MessageNamespace("InventoryUpdated")]
    public class InventoryUpdated : ICommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ICorrelationContext Context { get; set; }
    }
}
