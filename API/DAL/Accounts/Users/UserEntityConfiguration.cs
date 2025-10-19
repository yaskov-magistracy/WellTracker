using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Accounts.Users;

internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(e => e.Login)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(e => e.HashedPassword)
            .IsRequired();

        builder
            .HasMany(e => e.FoodDiaries)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
    }
}