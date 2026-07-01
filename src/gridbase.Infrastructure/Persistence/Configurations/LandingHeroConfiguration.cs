using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;

namespace YourProject.Infrastructure.Persistence.Configurations
{
    public class LandingHeroConfiguration : IEntityTypeConfiguration<LandingHeroConfig>
    {
        public void Configure(EntityTypeBuilder<LandingHeroConfig> builder)
        {
            builder.ToTable("LandingHeroConfigs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(x => x.SliderImages)
                .WithOne()
                .HasForeignKey(x => x.LandingHeroConfigId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class HeroSliderImageConfiguration : IEntityTypeConfiguration<HeroSliderImage>
    {
        public void Configure(EntityTypeBuilder<HeroSliderImage> builder)
        {
            builder.ToTable("HeroSliderImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}