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
using Common.Redis;

namespace ProductService.Controllers.API
{
    [Route("api/product")]
    public class ProductController : BaseController
    {
      
        IDispatcher _dispatcher;
        public ProductController
            (
            IDispatcher dispatcher,
            ITracer tracer) : base(tracer)
        {
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

        [HttpPut()]
        [AppMetricCount(MetricName: "Put-product")]
        public async Task<IActionResult> Put(Product p)
        {
            var c = new PutProductCommand(p);
            await _dispatcher.SendAsync<PutProductCommand>(c);
            return Ok();
        }

        //[HttpGet]
        //[AppMetricCount(MetricName: "Get-allProduct")]
        //public async Task<Product[]> GetallProduct()
        //{
        //    var q = new GetAllQuery ();
        //    var products = await _dispatcher.QueryAsync<GetAllQuery, Product[]>(q);
        //    return products ;
        //}

        [HttpGet]
       // [Cache(600)]
        [AppMetricCount(MetricName: "Get-allProductByFiltration")]
        public async Task<Product[]> GetallProductByFiltration([FromQuery] QueryParams productparams)
        {
            var q = new GetByFiltrationQuery(productparams);
            var products = await _dispatcher.QueryAsync<GetByFiltrationQuery, Product[]>(q);
            return products;
        }

        [HttpGet]
        [Cache(600)]
        [Route("productcategory")]
        [AppMetricCount(MetricName: "Get-allProductCateory")]
        public async Task<ProductCategory[]> GetallCategory()
        {
            var q = new GetAllCategoryQuery();
            var productcategorys = await _dispatcher.QueryAsync<GetAllCategoryQuery, ProductCategory[]>(q);
            return productcategorys;
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
