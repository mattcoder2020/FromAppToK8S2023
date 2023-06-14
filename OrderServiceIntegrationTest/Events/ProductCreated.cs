using Common.Messages;
using Common.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Events
{
    [MessageNamespace("ProductCreated")]
    public class ProductCreated:IEvent
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Category { get; set; }
        public Decimal Price { get; set; }
        public ICorrelationContext Context { get; set ; }
    }
}
