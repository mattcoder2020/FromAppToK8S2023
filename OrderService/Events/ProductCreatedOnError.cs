using Common.Messages;
using System;

namespace OrderService.Events
{
    public class ProductCreatedOnErrorHandler
    {
        public IRejectedEvent OnError(IEvent ev, Exception exception)
        {
            return new ProductCreatedRejected {  Reason = exception.Message };
        }
    }
}
