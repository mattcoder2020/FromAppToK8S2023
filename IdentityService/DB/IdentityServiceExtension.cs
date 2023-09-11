using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DB
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService
            (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppIdentityDbContext>(opt =>
            opt.UseSqlite(config.GetConnectionString("identityconnection")));
            return services;
        }
    }
}
