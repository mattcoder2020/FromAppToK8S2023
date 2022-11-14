using Common.Messages;
using Common.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Events
{
    public class ProductCreatedRejected:IRejectedEvent
    {
        public string  Reason { get; set; }
        public string Code { get; set; }
        public ICorrelationContext Context { get; set; }
    }
}
