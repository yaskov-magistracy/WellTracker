using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.StaticFiles;

public class StaticFileEntityConfiguration : IEntityTypeConfiguration<StaticFileEntity>
{
    public void Configure(EntityTypeBuilder<StaticFileEntity> builder)
    {
        builder.HasKey(p => p.Id);
    }
}