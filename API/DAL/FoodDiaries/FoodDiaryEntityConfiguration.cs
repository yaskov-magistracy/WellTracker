using DAL.EfCoreExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.FoodDiaries;

internal class FoodDiaryEntityConfiguration : IEntityTypeConfiguration<FoodDiaryEntity>
{
    public void Configure(EntityTypeBuilder<FoodDiaryEntity> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.FoodDiaries)
            .HasForeignKey(e => e.UserId);

        builder.HasConversionInJson(e => e.Breakfast);
        builder.HasConversionInJson(e => e.Lunch);
        builder.HasConversionInJson(e => e.Snack);
        builder.HasConversionInJson(e => e.Dinner);
        builder.OwnsOne(e => e.TotalNutriments);
        builder.OwnsOne(e => e.TotalEnergy);
    }
}