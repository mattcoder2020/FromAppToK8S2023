using System;
using System.Collections.Generic;
using Autofac;
using Common.Dispatcher;
using Common.Messages;
using Common.Metrics;
using Common.RabbitMQ;
using Common.Web;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using ProductService.Commands;

namespace ProductService.Controllers.API
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : BaseController
    {
        IComponentContext _componentContext;
        IDispatcher _dispatcher;
        public ProductController
            (IComponentContext componentContext,
            IDispatcher dispatcher,
            //IBusPublisher busPublisher,
            ITracer tracer) : base( tracer)
        {
            _componentContext = componentContext; _dispatcher = dispatcher;
        }
 
        // POST: api/Product
        [HttpPost]
        [AppMetricCount(MetricName: "post-new-product")]
        public async void Post([FromBody] NewProductCommand value)
        {
            value.Context = GetContext<NewProductCommand>(null, null);
            await _dispatcher.SendAsync<NewProductCommand>(value);
        }
    }
}
