﻿using Common.Messages;
using System;

namespace OrderService.Events
{
    [SubscriptionNamespace("Matt-Order")]
    [MessageNamespace("Matt-Product")]
    public class ProductUpdated : IEvent
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Category { get; set; }
        public Decimal Price { get; set; }
        public ICorrelationContext Context { get; set; }
    }
}
