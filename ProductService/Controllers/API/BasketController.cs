using Common.Redis;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.API.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IRedisRepository<Basket> redisrepo;

        public BasketController(IRedisRepository<Basket> redisrepo)
        {
            this.redisrepo = redisrepo;
        }
        // GET: api/<BasketController>
        [HttpGet("{basketid}")]
        public async Task<ActionResult<Basket>> Get(string basketid)
        {
            return await redisrepo.GetById(basketid);
        }

        //POST api/<BasketController>
        [HttpPost]
        public void Post([FromBody] Basket value)
        {
            //Basket b = JsonSerializer.Deserialize<Basket>(value);
            redisrepo.Add(value.basketid, value);
        }

        // POST api/<BasketController>
        //[HttpPost]
        //public void Post([FromBody] String value)
        //{
        //    //Basket b = JsonSerializer.Deserialize<Basket>(value);
        //    int ui = 1;
        //}
        // PUT api/<BasketController>/5
        [HttpPut("{basketid}")]
        public void Put(string basketid, [FromBody] string value)
        {
            Basket b = JsonSerializer.Deserialize<Basket>(value);
            redisrepo.Add(b.basketid, b);
        }

        // DELETE api/<BasketController>/5
        [HttpDelete("{basketid}")]
        public void Delete(string basketid)
        {
            redisrepo.DeleteById(basketid);
        }
    }
}
