using Consumer.Infrastructure.DAL.History.Models;
using Consumer.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Infrastructure.Context
{
    public class ConsumerContext: DbContext
    {
        // create migration: dotnet ef migrations Add $MigrationName -o Infrastructure\Migrations
        
        public ConsumerContext(DbContextOptions<ConsumerContext> options) : base(options)
        { }
        
        public DbSet<DbHistory> History { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(HistoryConfiguration).Assembly);

            base.OnModelCreating(builder);
        }
    }
}