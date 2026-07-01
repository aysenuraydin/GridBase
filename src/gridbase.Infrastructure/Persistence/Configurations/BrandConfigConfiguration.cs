using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;
namespace gridbase.Infrastructure.Persistence.Configurations;

public class BrandConfigConfiguration : IEntityTypeConfiguration<BrandConfig>
{
    public void Configure(EntityTypeBuilder<BrandConfig> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.CompanyName).IsRequired().HasMaxLength(200);
    }
}