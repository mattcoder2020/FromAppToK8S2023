using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers.API
{
    [Route("api/myapi")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        // GET: api/MyApi
        [HttpGet]
        [AppMetricCount(MetricName: "get-myapi")]
        public ActionResult Get()
        {
            return Ok(new { Name = "value2", Id=1 });
        }

        // GET: api/MyApi/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            return  Ok(new { Name = "value200", Id = id });
        }

        // POST: api/MyApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/MyApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
