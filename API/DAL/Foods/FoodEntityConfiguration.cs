using DAL.EfCoreExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Foods;

internal class FoodEntityConfiguration : IEntityTypeConfiguration<FoodEntity>
{
    public void Configure(EntityTypeBuilder<FoodEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.OwnsOne(e => e.Nutriments);
        
        builder.OwnsOne(e => e.Energy);

        builder.HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "russian",  // Text search config
                p => new { p.Name, p.BrandName })  // Included properties
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN"); 
        // builder.HasFullTextSearchIndex(
        //         e => e.SearchVector,
        //         e => new {e.Name, e.BrandName});
    }
}