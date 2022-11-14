using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ProductService.Models;
using ProductService.Query;
using ProductService.QueryHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.AutofacTest;

namespace ProductService.Controllers
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
            var dto = new DTO();
            var controller = new ApiFileSendingController(dto);
            controller.Send(null);
            return Ok("123");
        }
   
        //[HttpGet]
        //public IActionResult GetAll([FromQuery] GetAllQuery query)
        //{
        //    var handleType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(List<DemoModel>));
        //    dynamic QueryHandler = _context.Resolve(handleType);
        //    return new JsonResult(QueryHandler.execute((dynamic)query));
        // }

        [HttpPost]
        [Route("One")]
        public IActionResult GetOne([FromQuery] GetAllQuery query)
        {
            var handleType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(List<DemoModel>));
            dynamic QueryHandler = _context.Resolve(handleType);
            return new JsonResult(QueryHandler.execute((dynamic)query));
        }
    }
}