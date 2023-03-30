using Common.Messages;
using Common.RabbitMQ;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Commands
{
    public class NewProductCommand : ICommand
    {
            public int Id { get; set; }
            public String Name { get; set; }
            public int ProductCategoryId { get; set; }
            public decimal Price { get; set; }
            public ICorrelationContext Context { get; set; }
    }
}
