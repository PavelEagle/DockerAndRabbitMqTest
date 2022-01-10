using Consumer.Infrastructure.DAL;
using Consumer.Infrastructure.DAL.History.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consumer.Infrastructure.EntityConfigurations
{
    public class HistoryConfiguration: IEntityTypeConfiguration<DbHistory>
    {
        public void Configure(EntityTypeBuilder<DbHistory> builder)
        {
            builder.ToTable("History");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Description).HasMaxLength(256);
            builder.Property(b => b.Title).HasMaxLength(256);
            
            builder.Property(b => b.CreatedAt).IsRequired().HasMaxLength(7).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}