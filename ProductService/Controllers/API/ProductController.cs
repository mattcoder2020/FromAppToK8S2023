using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Common.Dispatcher;
using Common.Messages;
using Common.Metrics;
using Common.RabbitMQ;
using Common.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OpenTracing;
using ProductService.Commands;
using ProductService.Models;
using ProductService.Query;

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
            ITracer tracer) : base(tracer)
        {
            _componentContext = componentContext;
            _dispatcher = dispatcher;
        }

        // POST: api/Product
        [HttpPost]
        [AppMetricCount(MetricName: "post-new-product")]
        public async void Post([FromBody] NewProductCommand value)
        {
            value.Context = GetContext<NewProductCommand>(null, null);
            await _dispatcher.SendAsync<NewProductCommand>(value);
        }

        [HttpGet("{id}")]
        [AppMetricCount(MetricName: "Get-product")]
        public async Task<Product> Get(int id)
        {
            //value.Context = GetContext<NewProductCommand>(null, null);
            var q = new GetOneQuery { Id = id };
            return await _dispatcher.QueryAsync<Product>(q);
            //await _dispatcher.SendAsync<NewProductCommand>(value);
        }


    }
}
