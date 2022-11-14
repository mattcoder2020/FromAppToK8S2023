using Common.Fabio;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Common.Consul
{
    public static class ConsulExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var servicebuilder = services.BuildServiceProvider())
            {
                configuration = servicebuilder.GetService<IConfiguration>();
            }

            var options = new ConsulOptions();
            configuration.GetSection("Consul").Bind(options);
            services.Configure<ConsulOptions>(configuration.GetSection("consul"));
            services.Configure<FabioOptions>(configuration.GetSection("fabio"));
            services.AddTransient<IConsulServicesRegistry, ConsulServiceRegistry>();
            services.AddTransient<ConsulServiceDiscoveryMessageHandler>();
            services.AddHttpClient<IConsulHttpClient, ConsulHttpClient>()
                .AddHttpMessageHandler<ConsulServiceDiscoveryMessageHandler>();

            return services.AddSingleton<IConsulClient>(c =>
            new ConsulClient(cfg => cfg.Address = new System.Uri(options.url)));
            

        }

        public static string UseConsul(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var consulOptions = scope.ServiceProvider.GetService<IOptions<ConsulOptions>>();
                var fabioOptions = scope.ServiceProvider.GetService<IOptions<FabioOptions>>();
                var enabled = consulOptions.Value.enabled;
                var consulEnabled = Environment.GetEnvironmentVariable("CONSUL_ENABLED")?.ToLowerInvariant();
                if (!string.IsNullOrWhiteSpace(consulEnabled))
                {
                    enabled = consulEnabled == "true" || consulEnabled == "1";
                }

                if (!enabled)
                {
                    return string.Empty;
                }


                var address = consulOptions.Value.address;
                if (string.IsNullOrWhiteSpace(address))
                {
                    throw new ArgumentException("Consul address can not be empty.",
                        nameof(consulOptions.Value.pingEndpoint));
                }

                var uniqueId = Guid.NewGuid();
                var client = scope.ServiceProvider.GetService<IConsulClient>();
                var serviceName = consulOptions.Value.service;
                var serviceId = $"{serviceName}:{uniqueId}";
                var port = consulOptions.Value.port;
                var pingEndpoint = consulOptions.Value.pingEndpoint;
                var pingInterval = consulOptions.Value.pingInterval <= 0 ? 5 : consulOptions.Value.pingInterval;
                var removeAfterInterval =
                    consulOptions.Value.removeAfterInterval <= 0 ? 10 : consulOptions.Value.removeAfterInterval;
                var registration = new AgentServiceRegistration
                {
                    Name = serviceName,
                    ID = serviceId,
                    Address = address,
                    Port = port,
                    //Tags = null
                    Tags = fabioOptions.Value.Enabled ? GetFabioTags(serviceName, fabioOptions.Value.Service) : null
                };
                if (consulOptions.Value.pingEnabled)// || fabioOptions.Value.Enabled)
                {
                    var scheme = address.StartsWith("http", StringComparison.InvariantCultureIgnoreCase)
                        ? string.Empty
                        : "http://";
                    var check = new AgentServiceCheck
                    {
                        Interval = TimeSpan.FromSeconds(pingInterval),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(removeAfterInterval),
                        HTTP = $"{scheme}{address}{(port > 0 ? $":{port}" : string.Empty)}/{pingEndpoint}"
                    };
                    registration.Checks = new[] { check };
                }

                client.Agent.ServiceRegister(registration);

                return serviceId;
            }
        }

        private static string[] GetFabioTags(string consulService, string fabioService)
        {
            var service = (string.IsNullOrWhiteSpace(fabioService) ? consulService : fabioService)
                .ToLowerInvariant();

            return new[] { $"urlprefix-/{service} strip=/{service}" };
        }
    }
}
