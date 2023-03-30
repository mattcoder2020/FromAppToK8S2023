using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderService.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductService Productservice { get; }

        public ProductController(IProductService productservice)
        {
            Productservice = productservice;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return new Product[2];
        }

        //GET api/<ValuesController>/5
        //generate unit test for this httpget method


        [HttpGet("{id}")]
        public async Task<String> Get(int id)
        {
            var product = await Productservice.GetProductById(id);
            return JsonSerializer.Serialize(product);
        }

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public async Task<Product> Get(int id)
        //{
        //    var product = await Productservice.GetProductById(id);
        //    return product;
        //}

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
