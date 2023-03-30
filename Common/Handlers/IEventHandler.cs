using Common.Messages;
using Common.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Handlers
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task<Task> HandleAsync(TEvent @event, ICorrelationContext context);
    }

    //public interface IEventHandler<TEvent, TDbContext> where TEvent : IEvent where TDbContext : DbContext
    //{
    //    Task<Task> HandleAsync(TEvent @event, TDbContext dbcontext, ICorrelationContext context);
    //}
}
