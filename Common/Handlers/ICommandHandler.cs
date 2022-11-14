using Common.Messages;
using Common.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand:ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }

   
}
