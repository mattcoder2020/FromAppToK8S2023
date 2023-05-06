using Autofac;
using Common.Dispatcher;
using Common.Metrics;
using Common.Redis;
using Common.Web;
using InventoryService.Models;
using InventoryService.Service;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Threading.Tasks;

namespace ProductService.Controllers.API
{
    [Route("api/product")]
    public class ProductController : BaseController
    {

        IProductService _productservice;
        public ProductController
            (IComponentContext componentContext,
            IProductService productservice,
            ITracer tracer) : base(tracer)
        {
              _productservice = productservice;
        }

        [HttpPut()]
        [AppMetricCount(MetricName: "Put-product")]
        public async Task<IActionResult> Put(Product p)
        {
           await _productservice.PutByProductId(p.Id, p.Quantity, null);
           return Ok();
        }

        //[HttpGet]
        //[AppMetricCount(MetricName: "Get-allProduct")]
        //public async Task<Product[]> GetallProduct()
        //{
        //    var products = await _productservice.GetAll(null);
        //    return products;
        //}

        [HttpGet]
       // [Cache(600)]
        [AppMetricCount(MetricName: "Get-allProductByFiltration")]
        public async Task<Product[]> GetallProductByFiltration([FromQuery] QueryParams productparams)
        {
           var products = await _productservice.GetByFiltration(productparams, null);
            return products;
        }

     

    }
}
