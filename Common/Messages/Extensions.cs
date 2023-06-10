using Autofac;
using Common.Exceptions;
using Common.Handlers;
using Common.Jaeger;
using Common.Messages.AzureServiceBus;
using Common.Messages.RabbitMQ;
using Common.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using OpenTracing;
using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Instantiation;
using RawRabbit.Configuration;
using RawRabbit.Pipe.Middleware;
using RawRabbit.Common;
using System.Threading.Tasks;
using RawRabbit.Pipe;
using System.Threading;
using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using System.Linq;
using Microsoft.Azure.ServiceBus;

namespace Common.Messages
{
    public static class Extensions
    {
        static bool useRabbitMQ = false;
        static bool useAzureServiceBus = false;
        public static void AddMessageService(this ContainerBuilder builder, IServiceCollection collection)
        {
            var conf = collection.BuildServiceProvider().GetService<IConfiguration>();

            var optionsrawRabbitmq = conf.GetOptions<RawRabbitConfiguration>("rabbitMq");
            var optionsRabbitmq = conf.GetOptions<RabbitMqOptions>("rabbitMq");
            useRabbitMQ = optionsRabbitmq.Enable;
            var optionsAzureBusmq = conf.GetOptions<AzureBusOptions>("azureServiceBus");
            useAzureServiceBus = optionsAzureBusmq.Enable;

            builder.Register(context =>
            {
                return optionsRabbitmq;
            }).SingleInstance();

            builder.Register(context =>
            {
                return optionsAzureBusmq;
            }).SingleInstance();

            builder.Register(context =>
            {
                return optionsrawRabbitmq;
            }).SingleInstance();

            if ((useRabbitMQ && useAzureServiceBus) || (!useRabbitMQ && !useAzureServiceBus))
                throw new ApplicationException("Only one bus configuration need to be set up");

            var assembly = Assembly.GetCallingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerDependency();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerDependency();
            builder.RegisterType<Handler>().As<IHandler>()
                .InstancePerDependency();
            builder.RegisterInstance(DefaultTracer.Create()).As<ITracer>().SingleInstance()
                .PreserveExistingDefaults();

            ConfigureBus(builder);
        }

        private static void ConfigureBus(ContainerBuilder builder)
        {
            if (useRabbitMQ)
            {
                builder.RegisterType<RabbitMQBrokerFactory>()
                .As<IMessageBrokerFactory>()
                .InstancePerDependency();

                builder.RegisterType<BusPublisher>()
                .As<IBusPublisher>()
                .InstancePerDependency();

                ConfigureRabbitClient(builder);
            }
            if (useAzureServiceBus)
            {
                builder.RegisterType<AzureBusBrokerFactory>()
                .As<IMessageBrokerFactory>()
                .InstancePerDependency();

                builder.RegisterType<AzureBusPublisher>()
                .As<IBusPublisher>()
                .InstancePerDependency();
            }
        }

        private static void ConfigureRabbitClient(ContainerBuilder builder)
        {
            builder.Register<IInstanceFactory>(context =>
            {
                var options = context.Resolve<RabbitMqOptions>();
                var configuration = context.Resolve<RawRabbitConfiguration>();
                var namingConventions = new CustomNamingConventions(options.Namespace);
                var tracer = context.Resolve<ITracer>();

                return RawRabbitFactory.CreateInstanceFactory(new RawRabbitOptions
                {
                    DependencyInjection = ioc =>
                    {
                        ioc.AddSingleton(options);
                        ioc.AddSingleton(configuration);
                        ioc.AddSingleton<INamingConventions>(namingConventions);
                        ioc.AddSingleton(tracer);
                    },
                    Plugins = p => p
                        .UseAttributeRouting()
                        .UseRetryLater()
                        .UpdateRetryInfo()
                        .UseMessageContext<CorrelationContext>()
                        .UseContextForwarding()
                        .UseJaeger(tracer)
                });
            }).SingleInstance();
            builder.Register(context => context.Resolve<IInstanceFactory>().Create());
        }

        public static IBusSubscriber UseMessageService(this IApplicationBuilder builder)
        {
            IServiceProvider _serviceProvider = builder.ApplicationServices.GetService<IServiceProvider>();
            var messageBrokerFactory = _serviceProvider.GetService<IMessageBrokerFactory>();
            return messageBrokerFactory.Subscriber;


        }

        public static IBusSubscriber SubscribeAllMessages<TMessage>
            (this IBusSubscriber subscriber, string subscribeMethod, IApplicationBuilder builder)
        {
            //Get all the message types from the entry assembly
            var messageTypes = Assembly.GetEntryAssembly().GetTypes().AsQueryable()
                .Where(t => t.IsClass && typeof(TMessage).IsAssignableFrom(t))
                .ToList();

            // Iterate through all IEvent implementation class and fetch its topic subscription attribute / namespace attribute
            // and subscribe to the topic dynamically so that we don't have to subscribe to each topic manually
            
            
            messageTypes.ForEach(mt => subscriber.GetType()
                .GetMethod(subscribeMethod)
                .MakeGenericMethod(mt)
                .Invoke(subscriber,
                    new object[] {
                        mt.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace,
                        mt.GetCustomAttribute<SubscriptionNamespaceAttribute>()?.Namespace,
     // one tricky thing here is that to get a delegate of the OnError method of the event handler
     // here we pass in builder to retrieve the eventhandler instance from the DI container base on eventtype
                        OnError(mt, builder)
        }));
            return subscriber;
        }

        private static Delegate OnError(Type eventtype, IApplicationBuilder builder)
        {
            IComponentContext _context = builder.ApplicationServices.GetService<IComponentContext>();
            var handleType = typeof(IEventOnErrorHandler<>).MakeGenericType(eventtype);
            object obj;
            try
            {
                obj = _context.Resolve(handleType);
            }
            catch
            {
                return null;
            }
            Type ht = obj.GetType();
            MethodInfo method = ht.GetMethod("OnError");
                  
            return Delegate.CreateDelegate(typeof(Func<,,>).MakeGenericType(eventtype, typeof(Exception), typeof(IRejectedEvent)), null, method);
        }

        private class CustomNamingConventions : NamingConventions
        {
            public CustomNamingConventions(string defaultNamespace)
            {
                ExchangeNamingConvention = type => GetNamespace(type, defaultNamespace).ToLowerInvariant();
                RoutingKeyConvention = type =>
                    $"#.{GetRoutingKeyNamespace(type, defaultNamespace)}{type.Name.Underscore()}".ToLowerInvariant();
                ErrorExchangeNamingConvention = () => $"{defaultNamespace}.error";
                RetryLaterExchangeConvention = span => $"{defaultNamespace}.retry";
                RetryLaterQueueNameConvetion = (exchange, span) =>
                    $"{defaultNamespace}.retry_for_{exchange.Replace(".", "_")}_in_{span.TotalMilliseconds}_ms".ToLowerInvariant();
            }

            private static string GetRoutingKeyNamespace(Type type, string defaultNamespace)
            {
                var @namespace = type.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ?? defaultNamespace;

                return string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";
            }

            private static string GetNamespace(Type type, string defaultNamespace)
            {
                var @namespace = type.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ?? defaultNamespace;

                return string.IsNullOrWhiteSpace(@namespace) ? "#" : $"{@namespace}";
            }
        }

        private class RetryStagedMiddleware : StagedMiddleware
        {
            public override string StageMarker { get; } = RawRabbit.Pipe.StageMarker.MessageDeserialized;

            public override async Task InvokeAsync(IPipeContext context,
                CancellationToken token = new CancellationToken())
            {
                var retry = context.GetRetryInformation();
                if (context.GetMessageContext() is CorrelationContext message)
                {
                    message.Retries = retry.NumberOfRetries;
                }

                await Next.InvokeAsync(context, token);
            }
        }

        private static IClientBuilder UpdateRetryInfo(this IClientBuilder clientBuilder)
        {
            clientBuilder.Register(c => c.Use<RetryStagedMiddleware>());

            return clientBuilder;
        }
    }
}
  

    

    

