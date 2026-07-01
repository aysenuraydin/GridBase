using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;
using gridbase.Domain.Enums;

namespace gridbase.Infrastructure.Persistence.Configurations;

public class RulesValidationConfiguration
    : IEntityTypeConfiguration<RulesValidationConfig>
{
    public void Configure(EntityTypeBuilder<RulesValidationConfig> builder)
    {
        builder.ToTable("RulesValidationConfigs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rule)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Value)
            .HasMaxLength(500);

        builder.Property(x => x.Message)
            .HasMaxLength(500);

        builder.HasQueryFilter(c => c.DeletedAt == null);
    }
}