using Common.Messages;
using Common.RabbitMQ;
using System;

namespace ProductService.Commands
{
    public class NewProductCommand : ICommand
    {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Category { get; set; }
            public decimal Price { get; set; }

            public ICorrelationContext Context { get; set; }
    }
}
