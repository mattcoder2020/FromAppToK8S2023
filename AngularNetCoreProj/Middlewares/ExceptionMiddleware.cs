using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Threading.Tasks;
using System;



namespace AngularNetCoreProj.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostingEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostingEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
              
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string content = "something wrong";
                //if (env.IsDevelopment())
                //    content = JsonSerializer.Serialize(new { code = (int)HttpStatusCode.InternalServerError, message = ex.Message, stacktrace = ex.StackTrace });
                //else
                //    content = JsonSerializer.Serialize(new { code = (int)HttpStatusCode.InternalServerError, message = ex.Message });
                await context.Response.WriteAsync(content);
            }
        }
    }
}
