using Common.Messages;
using Common.RabbitMQ;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Commands
{
    public class DeleteProductCommand : ICommand
    {
            public int Id { get; set; }
            public ICorrelationContext Context { get; set; }
    }
}
