using Domain.Chats;

namespace DAL.Chats;

internal static class ChatsMapper
{
    public static Chat ToDomain(ChatEntity entity)
        => new(entity.Id, entity.Title, entity.UserId);

    public static ChatMessage ToDomain(ChatMessageEntity entity)
        => new(entity.Id, entity.DateTime, entity.Message, entity.IsBot);
}