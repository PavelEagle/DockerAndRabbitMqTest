using Consumer.Core.Abstractions.History;
using Consumer.Core.Services;
using Consumer.Infrastructure.DAL.History;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCustomServices(this IServiceCollection services)
        {
            // services
            services.AddScoped<IHistoryService, HistoryService>();
            
            // repositories
            services.AddScoped<IHistoryRepository, HistoryRepository>();

            return services;
        }
    }
}