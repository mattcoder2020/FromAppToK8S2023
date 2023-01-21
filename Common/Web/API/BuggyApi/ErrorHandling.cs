using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Web.API.BuggyApi
{
    [Route("Error/{code}")]
    public class ErrorHandling: ControllerBase
    {
        public IActionResult Handling (int code)
        {
            return new ObjectResult(new { code = code, message = GenerateMessage(code) });  
        }

        private string GenerateMessage(int code)
        {
            switch (code)
            { 
                case 400: return "You attempted a bad request";
                case 401: return "You are not authorized for this request";
                case 404: return "Resource Not Found";
                case 500: return "Server side error";

                default: return "Some unexpected error occured";
            }

        }
    }
}
