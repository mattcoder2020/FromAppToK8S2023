using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using OrderService.Models;
using OrderService.Query;
using OrderService.QueryHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DIController : ControllerBase
    {
        private readonly IComponentContext _context; 
        public DIController(IComponentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult foo()
        {
            //throw new ApplicationException("Gotcha");
            return Ok("123");
        }
   
        [HttpPost]
        public IActionResult GetAll([FromBody] GetAllQuery query)
        {
            var handleType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(List<DemoModel>));
            dynamic QueryHandler = _context.Resolve(handleType);
            return new JsonResult(QueryHandler.execute((dynamic)query));
              }
    }
}