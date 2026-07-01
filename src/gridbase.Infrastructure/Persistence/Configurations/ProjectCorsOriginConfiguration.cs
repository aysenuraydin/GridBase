using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;

namespace gridbase.Infrastructure.Persistence.Configurations;

public class ProjectCorsOriginConfiguration : IEntityTypeConfiguration<ProjectCorsOrigin>
{
    public void Configure(EntityTypeBuilder<ProjectCorsOrigin> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Origin).IsRequired().HasMaxLength(300);

        builder.HasOne(o => o.ProjectFk)
            .WithMany()
            .HasForeignKey(o => o.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(o => new { o.ProjectId, o.Origin }).IsUnique();
    }
}