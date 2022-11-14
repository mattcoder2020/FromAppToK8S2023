using Common.Dispacher;
using Common.Messages;
using Common.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Common.Dispatcher
{
    public interface IDispatcher 
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand:ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query) ;

    }
}
