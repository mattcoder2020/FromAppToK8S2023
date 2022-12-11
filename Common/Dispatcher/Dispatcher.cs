using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Dispacher;
using Common.Messages;


namespace Common.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        ICommandDispatcher _commandDispatcher;
        IQueryDispatcher _queryDispacher;

        public Dispatcher(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispacher = queryDispatcher;
        }
        public Task<TResult> QueryAsync<TQuery,TResult>(TQuery query) where TQuery: IQuery
        {
            return _queryDispacher.Query<TQuery,TResult>(query);
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _commandDispatcher.Dispatch<TCommand>(command);
           
        }
    }
}
