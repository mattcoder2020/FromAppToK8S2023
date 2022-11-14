using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Dispacher;
using Common.Messages;
using Common.Types;

namespace Common.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        ICommandDispatcher _commandDispatcher;
        public Dispatcher(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _commandDispatcher.Dispatch<TCommand>(command);
           
        }
    }
}
