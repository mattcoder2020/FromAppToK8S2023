using System.Threading.Tasks;
using Autofac;
using Common.Handlers;
using Common.Messages;


namespace Common.Dispacher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private IComponentContext _context;
        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task Dispatch<TCommand>(TCommand command ) where TCommand : ICommand
        {
            var CommandHandler = _context.Resolve<ICommandHandler<TCommand>>();
            //await CommandHandler.HandleAsync(command, CorrelationContext.Empty);
            await CommandHandler.HandleAsync(command, command.Context);
            //  return Task.CompletedTask;

        }
    }
}
