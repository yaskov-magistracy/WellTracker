using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Chats;

internal class ChatMessageEntityConfiguration : IEntityTypeConfiguration<ChatMessageEntity>
{
    public void Configure(EntityTypeBuilder<ChatMessageEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(e => e.Chat)
            .WithMany(e2 => e2.Messages)
            .HasForeignKey(e => e.ChatId);
    }
}