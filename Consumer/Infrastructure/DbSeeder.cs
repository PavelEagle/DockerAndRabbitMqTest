using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace Consumer.Infrastructure
{
    public class DbSeeder
    {
        private readonly DbContext _context;
        private readonly ILogger<DbSeeder> _logger;

        public DbSeeder(DbContext context, ILogger<DbSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task MigrateAndSeedAsync()
        {
            const byte retryCount = 2;
            const byte retryDuration = 15;

            var fallback = Policy
                .Handle<Exception>()
                .FallbackAsync(_ => Task.CompletedTask, async ex =>
                {
                    await Task.FromResult(true);
                    _logger.LogError($"Error during migration: {ex.Message}");
                });

            var retry = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(retryCount, x => TimeSpan.FromSeconds(retryDuration));

            await fallback.WrapAsync(retry)
                .ExecuteAsync(async () =>
                {
                    _logger.LogInformation("DB: {DbName}", _context.Database.GetDbConnection().Database);
                    _logger.LogInformation("Migrate and seed DB...");
                    await _context.Database.MigrateAsync();
                    _logger.LogInformation("Migrated...");
                });
        }
    }
}