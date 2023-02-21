using Common.Messages;
using ProductService.Models;

namespace ProductService.Commands
{
    public class PutProductCommand: ICommand
    {
        private Product p;

        public PutProductCommand(Product p)
        {
            this.p = p;
        }

        public ICorrelationContext Context { get; set ; }
        public Product Product { get => p; }
    }
}