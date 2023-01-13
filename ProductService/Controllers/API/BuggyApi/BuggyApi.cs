using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Controllers.API.BuggyApi
{
    [Route("api/buggy")]
    public class BuggyApi: ControllerBase
    {
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            int[] errorArray = new int[2] { 1, 2 };
            String teststring = "matt";
            int errorVar = int.Parse(teststring);
            return Ok();
        }

        [HttpGet("autherror")]
        [Authorize]
        public ActionResult GetAuthError()
        {
            return Ok();
        }
    }
}
