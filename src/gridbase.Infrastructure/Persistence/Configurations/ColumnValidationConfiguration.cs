using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;

namespace gridbase.Infrastructure.Persistence.Configurations;

public class ColumnValidationConfiguration
    : IEntityTypeConfiguration<ColumnValidationConfig>
{
    public void Configure(EntityTypeBuilder<ColumnValidationConfig> builder)
    {
        builder.ToTable("ColumnValidationConfigs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .IsRequired();
        builder.HasMany(c => c.Rules)
            .WithOne()
            .HasForeignKey(r => r.ColumnValidationConfigId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(c => c.DeletedAt == null);
    }
}