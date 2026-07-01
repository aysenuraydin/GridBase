using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;

namespace gridbase.Infrastructure.Persistence.Configurations;

public class DatatableConfiguration : IEntityTypeConfiguration<Datatable>
{
    public void Configure(EntityTypeBuilder<Datatable> builder)
    {
        builder.ToTable(nameof(Datatable), "dbo");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(t => t.ColumnsFk)
            .WithOne(c => c.TableFk)
            .HasForeignKey(c => c.TableId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.RowsFk)
            .WithOne(r => r.TableFk)
            .HasForeignKey(r => r.TableId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.ForeignTablesFk)
            .WithOne(ft => ft.DatatableFk)
            .HasForeignKey(ft => ft.DatatableId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.ProjectFk)
            .WithMany()
            .HasForeignKey(d => d.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(d => new { d.ProjectId, d.Name }).IsUnique();

    }
}