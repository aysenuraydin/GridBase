using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gridbase.Domain.Entities;
namespace gridbase.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), "USR");

        builder.Property(b => b.Username).HasMaxLength(30).IsRequired();
        builder.Property(b => b.Email).HasMaxLength(150).IsRequired();
        builder.Property(b => b.PasswordHash).HasMaxLength(150).IsRequired();
    }
}