using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;
namespace gridbase.Infrastructure.Persistence.Configurations;

public class StatsSectionConfiguration : IEntityTypeConfiguration<StatsSection>
{
    public void Configure(EntityTypeBuilder<StatsSection> e)
    {
        e.HasKey(x => x.Id);
    }
}