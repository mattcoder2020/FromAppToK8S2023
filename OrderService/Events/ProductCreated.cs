using Common.Messages;
using Common.RabbitMQ;
using System;

namespace OrderService.Events
{
    [SubscriptionNamespace("Matt-Product")]
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

        public ProductCreatedRejected OnError(ProductCreated p, Exception exception)
        {
            return new ProductCreatedRejected { Code = p.Id.ToString(), Reason = exception.Message };
        }
    }
}
