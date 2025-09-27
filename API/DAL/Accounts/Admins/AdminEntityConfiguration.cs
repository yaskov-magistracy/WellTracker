using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Accounts.Admins;

internal class AdminEntityConfiguration : IEntityTypeConfiguration<AdminEntity>
{
    public void Configure(EntityTypeBuilder<AdminEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(e => e.Login)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(e => e.HashedPassword)
            .IsRequired();
    }
}