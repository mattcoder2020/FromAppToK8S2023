using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Redis
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        RedisRepository<string> redis;
        public CacheAttribute(int secstolive)
        {
            Secstolive = secstolive;
            
        }

        private readonly int Secstolive;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var multiplexer = context.HttpContext.RequestServices.GetRequiredService<IConnectionMultiplexer>();
            redis = new RedisRepository<string>(multiplexer);
            string keyname = GeneratedKeyFromHttpRequest(context);
            string cachedResult = await redis.GetStringById(keyname);
            if (!string.IsNullOrEmpty(cachedResult) && cachedResult.Length > 3)
            {
                context.Result = new ContentResult { Content = cachedResult, StatusCode = 200, ContentType = "applicaion/json" };
                return;
            }
            var executeResult = await next();  // No cached found and return? move next to execute the controller handler
            if(executeResult != null)
            {
                if (executeResult.Result is ObjectResult okObjectResult)
                {
                    await redis.Add(keyname, okObjectResult.Value, Secstolive);
                }
            }
        }

        private string GeneratedKeyFromHttpRequest(ActionExecutingContext context)
        {
            var keyBuilder = new StringBuilder();
            var request = context.HttpContext.Request;
            keyBuilder.Append($"{request.Path}");

           // request.Query.AsQueryable().ForEachAsync(e => keyBuilder.Append($"|{e.Key}-{e.Value}"));
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
