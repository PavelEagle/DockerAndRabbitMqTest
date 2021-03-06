using System;
using System.Runtime.CompilerServices;
using Consumer.Bus.Consumers;
using EventBus;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Consumer.Bus
{
    public static class ConfigureMassTransitExtension
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
        {
            var busConfiguration = GetBusConfiguration(services);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<HistoryEventConsumer>();
                x.AddConsumer<HistoryRequestConsumer>();

                x.AddBus(context => MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(busConfiguration.Host), h =>
                    {
                        h.Username(busConfiguration.UserName);
                        h.Password(busConfiguration.Password);
                    });

                    cfg.DefaultReceiveEndpoint<HistoryEventConsumer>(QueueNames.TestQueue, context);
                    cfg.DefaultReceiveEndpoint<HistoryRequestConsumer>(QueueNames.TestRequestQueue, context);
                }));
            });

            services.AddMassTransitHostedService();

            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BusConfiguration GetBusConfiguration(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IOptions<BusConfiguration>>().Value;
        }

        private static void DefaultReceiveEndpoint<T>(
            this IRabbitMqBusFactoryConfigurator configuration, string queueName, IRegistration context)
        where T: class, IConsumer
        {
            configuration.ReceiveEndpoint(queueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<T>(context);
            });
        }
    }
}