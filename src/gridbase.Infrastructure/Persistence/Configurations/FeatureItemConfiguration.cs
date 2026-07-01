using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;

namespace gridbase.Infrastructure.Persistence.Configurations;

public class FeatureItemConfiguration : IEntityTypeConfiguration<FeatureItem>
{
    public void Configure(EntityTypeBuilder<FeatureItem> builder)
    {
        builder.ToTable("FeatureItems");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(f => f.SubTitle)
            .HasMaxLength(100);

        builder.Property(f => f.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(f => f.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(f => f.IconUrl)
            .HasMaxLength(100);

        builder.Property(f => f.OrderNumber)
            .IsRequired();

        builder.Property(f => f.IsRight)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(f => f.BgColor)
            .IsRequired()
            .HasMaxLength(50);
    }
}

// 2. FeatureDetail Tablosu Ayarları
public class FeatureDetailConfiguration : IEntityTypeConfiguration<FeatureDetail>
{
    public void Configure(EntityTypeBuilder<FeatureDetail> builder)
    {
        builder.ToTable("FeatureDetails");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Label)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(d => d.Value)
            .HasMaxLength(50);

        builder.HasOne(d => d.FeatureItem)
            .WithMany(i => i.FeaturesDetails)
            .HasForeignKey(d => d.FeatureItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class CtaConfigConfiguration : IEntityTypeConfiguration<CtaConfig>
{
    public void Configure(EntityTypeBuilder<CtaConfig> builder)
    {
        builder.ToTable("CtaConfigs");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Text)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(c => c.ButtonText)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.ButtonUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasData(new CtaConfig
        {
            Id = 1,
            Text = "Build your web App/SaaS with GridBase dashboard",
            ButtonText = "Buy Now",
            ButtonUrl = "/landing"
        });
    }
}