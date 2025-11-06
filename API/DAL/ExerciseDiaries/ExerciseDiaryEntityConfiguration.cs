using DAL.EfCoreExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ExerciseDiaries;

internal class ExerciseDiaryEntityConfiguration : IEntityTypeConfiguration<ExerciseDiaryEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseDiaryEntity> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.ExerciseDiaries)
            .HasForeignKey(e => e.UserId);

        builder.HasConversionInJson(e => e.Current);
    }
}