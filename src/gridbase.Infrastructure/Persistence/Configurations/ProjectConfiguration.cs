using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;
using gridbase.Infrastructure.Identity;

namespace gridbase.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.OwnerUserId)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.HasIndex(p => p.OwnerUserId);

        builder.HasOne<ApplicationUser>()
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.OwnerUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}