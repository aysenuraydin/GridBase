using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;

namespace gridbase.Infrastructure.Persistence.Configurations;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(k => k.KeyHash).IsRequired().HasMaxLength(128);
        builder.Property(k => k.KeyPrefix).IsRequired().HasMaxLength(64);
        builder.Property(k => k.Name).HasMaxLength(100);

        builder.HasIndex(k => k.KeyHash).IsUnique();

        builder.HasOne(k => k.ProjectFk)
            .WithMany()
            .HasForeignKey(k => k.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(k => k.ProjectId);
    }
}