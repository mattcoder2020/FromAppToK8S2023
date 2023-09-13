using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebIdentityService.Entity;

namespace WebIdentityService.DB
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService
            (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppIdentityDbContext>(opt =>
            opt.UseSqlite(config.GetConnectionString("identityconnection")));


            services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();
            services.AddAuthorization();

            return services;
        }
    }
}
