using DAL.EfCoreExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Exercises;

internal class ExerciseEntityConfiguration : IEntityTypeConfiguration<ExerciseEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasFullTextSearchIndex(
            e => e.SearchVector,
            e => new {e.Name, e.Description});
    }
}