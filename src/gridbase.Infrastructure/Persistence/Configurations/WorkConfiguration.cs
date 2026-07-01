using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;

public class WorkConfiguration : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Title).IsRequired().HasMaxLength(255);
        e.Property(x => x.Category).HasMaxLength(100);
        e.Property(x => x.Link).HasMaxLength(500);
    }
}