using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Jaeger;
using Common.Messages;
using Common.Metrics;
using Common.RabbitMQ;
using Common.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Events;
using OrderService.SQLiteDB;
using Unity;

namespace OrderService
{
    public class Startup
    {
        public IContainer Container { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OrderDBContext>(options => options.UseSqlite(connectionstring));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddGaugeMetric();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });
            // services.AddConsul();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            //builder.AddRabbitMq();
            builder.AddMessageService(services);
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //app.UseRabbitMq().SubscribeEvent<ProductCreated>(@namespace : "matt-product", 
            //    onError: (message, exception)=>new ProductCreatedRejected { Code=message.Id.ToString(), Reason=exception.Message} );
            app.UseMessageService().SubscribeAllMessages<IEvent>("SubscribeEvent", app);
            //how to make the function invoked using .net reflection method
                        
  //          app.UseMessageService().SubscribeEvent<ProductCreated>(@namespace: "Matt-Product", queueName: "Matt-Product",
  //onError: (message, exception) => new ProductCreatedRejected { Code = message.Id.ToString(), Reason = exception.Message });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors("CorsPolicy");


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

           
        }



    }
}
