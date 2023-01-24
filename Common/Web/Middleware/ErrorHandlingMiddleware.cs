using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment env;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate del, IHostingEnvironment env)
        {
            this.logger = logger;
            this.del = del;
            this.env = env;
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
                string content;
                if (env.IsDevelopment())
                     content = JsonSerializer.Serialize(new { code = (int)HttpStatusCode.InternalServerError, message = ex.Message, stacktrace = ex.StackTrace });
                else
                    content = JsonSerializer.Serialize(new { code = (int)HttpStatusCode.InternalServerError, message = ex.Message});
                await context.Response.WriteAsync(content);
            }
        }
    }
}
