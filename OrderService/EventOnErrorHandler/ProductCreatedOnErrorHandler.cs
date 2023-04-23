using Common.Handlers;
using Common.Messages;
using OrderService.Events;
using System;

namespace OrderService.EventOnErrorHandler
{
    public class ProductCreatedOnErrorHandler : IEventOnErrorHandler<ProductCreated>
    {
        public IRejectedEvent OnError(ProductCreated ev, Exception exception)
        {
            return new ProductCreatedRejected { Code = ev.Id.ToString(), Reason = exception.Message };
        }           
    }
}
