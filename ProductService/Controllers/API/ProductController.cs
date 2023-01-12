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
using ProductService.QueryHandler;

namespace ProductService.Controllers.API
{
    [Route("api/product")]
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
        public async Task<IActionResult> Post([FromBody] NewProductCommand value)
        {
            value.Context = GetContext<NewProductCommand>(null, null);
            await _dispatcher.SendAsync<NewProductCommand>(value);
            return Ok();
        }

        [HttpGet("{id}")]
        [AppMetricCount(MetricName: "Get-product")]
        public async Task<IActionResult> Get(int id)
        {
            //value.Context = GetContext<NewProductCommand>(null, null);
            var q = new GetOneQuery { Id = id };
            Product p = await _dispatcher.QueryAsync<GetOneQuery, Product>(q);
            return Single<Product>(p, p1 => { return p1 != null; });
            //await _dispatcher.SendAsync<NewProductCommand>(value);
        }

        [HttpDelete("{id}")]
        [AppMetricCount(MetricName: "Delete-product")]
        public async void Delete(int id)
        {
            //value.Context = GetContext<NewProductCommand>(null, null);
            var q = new DeleteProductCommand { Id = id };
             await _dispatcher.SendAsync<DeleteProductCommand>(q);
           
        }

    }
}
