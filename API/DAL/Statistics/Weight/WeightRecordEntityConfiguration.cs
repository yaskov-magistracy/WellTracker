using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Statistics.Weight;

internal class WeightRecordEntityConfiguration : IEntityTypeConfiguration<WeightRecordEntity>
{
    public void Configure(EntityTypeBuilder<WeightRecordEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(e => e.User)
            .WithMany(e2 => e2.WeightHistory)
            .HasForeignKey(e => e.UserId);
    }
}