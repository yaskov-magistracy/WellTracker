using DAL.EfCoreExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Foods;

public class FoodEntityConfiguration : IEntityTypeConfiguration<FoodEntity>
{
    public void Configure(EntityTypeBuilder<FoodEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.OwnsOne(e => e.Nutriments);
        
        builder.OwnsOne(e => e.Energy);

        builder.HasFullTextSearchIndex(
                e => e.SearchVector,
                e => new {e.Name, e.BrandName});
    }
}