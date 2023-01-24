using Common.Jaeger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Common.Messages;
using Common.Metrics;
using Common.Web.Middleware;
using Common.Swagger;

namespace InventoryService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddJaeger().  //Add singleton instance that has been configured to the IOC mapper
                AddOpenTracing().
                AddGaugeMetric();

            services.AddSwagger();

            //Leverage Autofac to add all types including the interface and implementation base on paraemeter type
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsImplementedInterfaces();
            builder.Populate(services);

            builder.AddMessageService(services);
            Container = builder.Build();
            return new AutofacServiceProvider(Container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseSwaggerA();

            //app.UseMessageService()    //get the messaging utility factory instance from the IOC mapper, either rabbitmq or azurebus 
            //    .SubscribeEvent<ProductCreated>(@namespace:"Matt-product",queueName:"Matt-product", 
            //    onError: (message, exception) =>  //Place a parameterized call back to the subscribe function
            //    new ProductCreatedRejected { Code = message.Id.ToString(), Reason = exception.Message });

            app.UseMvc();
        }
    }
}
