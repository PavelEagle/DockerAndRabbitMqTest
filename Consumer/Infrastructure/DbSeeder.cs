using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            try
            {
                _logger.LogInformation("DB: {DbName}", _context.Database.GetDbConnection().Database);
                _logger.LogInformation("Migrate and seed DB...");
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Migrated...");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during migration: {e.Message}");
            }
      
        }
    }
}