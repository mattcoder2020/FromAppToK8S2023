using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Events
{
    [SubscriptionNamespace("Matt-Inventory")]
    [MessageNamespace("Matt-Product")]
    public class ProductCreated : IEvent
    {
        public int Id { get; private set; }
        public String Name { get; set; }
        public int Category { get; set; }
        public decimal Price { get; set; }

        public ProductCreated(int id, String name, int productCategoryId, decimal price)
        {
            Id = id;
            Name = name;
            Category = productCategoryId;
            Price = price;
        }
        public ICorrelationContext Context { get; set; }

    }
}
