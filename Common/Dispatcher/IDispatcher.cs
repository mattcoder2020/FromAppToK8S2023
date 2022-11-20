using Common.Dispacher;
using Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Common.Dispatcher
{
    public interface IDispatcher 
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand:ICommand;
        TResult QueryAsync<TResult>(IQuery query) ;

    }
}
