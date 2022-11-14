using Common.Handlers;
using Common.Messages;
using Common.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dispacher
{
    public interface ICommandDispatcher
    {
         Task Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand;
     }
}
