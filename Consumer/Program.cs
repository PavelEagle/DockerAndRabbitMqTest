using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Consumer.Infrastructure;
using Consumer.Infrastructure.Context;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHost(args);
            
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ConsumerContext>();
                var seederLogger = scope.ServiceProvider.GetService<ILogger<DbSeeder>>();
                await new DbSeeder(context, seederLogger).MigrateAndSeedAsync();
            }
            
            await host.RunAsync();
        }

        private static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());
    }
}
