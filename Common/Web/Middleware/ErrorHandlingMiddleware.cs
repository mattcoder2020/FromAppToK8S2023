using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;


namespace Common.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly RequestDelegate del;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate del)
        {
            this.logger = logger;
            this.del = del;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await del(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var json = new { code = (int)HttpStatusCode.InternalServerError, message = ex.Message, stacktrace = ex.StackTrace };
                string content = JsonSerializer.Serialize(json);
                await context.Response.WriteAsync(content);


            }
        }
    }
}
