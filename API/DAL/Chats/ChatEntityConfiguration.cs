using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Chats;

internal class ChatEntityConfiguration : IEntityTypeConfiguration<ChatEntity>
{
    public void Configure(EntityTypeBuilder<ChatEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(e => e.User)
            .WithMany(e2 => e2.Chats)
            .HasForeignKey(e => e.UserId);
        
        builder.HasMany(e => e.Messages)
            .WithOne(e => e.Chat)
            .HasForeignKey(e => e.ChatId);
    }
}