using System;
using System.Runtime.CompilerServices;
using EventBus;
using EventBus.Extensions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Producer.Bus
{
    public static class ConfigureMassTransitExtension
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
        {
            var busConfiguration = GetBusConfiguration(services);
            
            BusExtensions.InitHost(busConfiguration.Host);

            services.AddMassTransit(x =>
            {
                x.AddBus(context => MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(busConfiguration.Host), h =>
                    {
                        h.Username(busConfiguration.UserName);
                        h.Password(busConfiguration.Password);
                    });
                }));
            });

            services.AddMassTransitHostedService();
            services.AddGenericRequestClient();

            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BusConfiguration GetBusConfiguration(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IOptions<BusConfiguration>>().Value;
        }
    }
}