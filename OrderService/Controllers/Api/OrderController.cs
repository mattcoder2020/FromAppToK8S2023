using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrderService orderService)
        {
            OrderService = orderService;
        }

        public IOrderService OrderService { get; }


        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<Order> Get(int id)
        {
            return await OrderService.GetOrderById(id);
        }   

        // POST api/<OrderController>
        // provide a unit test for this method

        [HttpPost]
         public void Post([FromBody] Order value)
        {
        }



    }
}