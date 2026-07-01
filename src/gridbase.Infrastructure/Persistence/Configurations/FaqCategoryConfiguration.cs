using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;
namespace gridbase.Infrastructure.Persistence.Configurations;

public class FaqCategoryConfiguration : IEntityTypeConfiguration<FaqCategory>
{
    public void Configure(EntityTypeBuilder<FaqCategory> e)
    {
        e.HasKey(x => x.Id);
        e.HasMany(x => x.Questions)
            .WithOne(x => x.FaqCategory)
            .HasForeignKey(x => x.FaqCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}