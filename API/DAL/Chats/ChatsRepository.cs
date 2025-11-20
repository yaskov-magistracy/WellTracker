using Domain.Chats;
using Domain.Chats.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Chats;

public class ChatsRepository(
    DataContext dataContext
) : IChatsRepository
{
    private DbSet<ChatEntity> Chats => dataContext.Chats;
    private DbSet<ChatMessageEntity> ChatMessages => dataContext.ChatMessages;
    
    public async Task<Chat> CreateChat(Guid userId, string title)
    {
        var chat = new ChatEntity()
        {
            Title = title,
            UserId = userId,
        };
        await Chats.AddAsync(chat);
        await dataContext.SaveChangesAsync();
        return ChatsMapper.ToDomain(chat);
    }

    public async Task<ChatMessage> SendMessage(Guid chatId, string message, bool isBot)
    {
        var chatMessage = new ChatMessageEntity()
        {
            Message = message,
            ChatId = chatId,
            DateTime = DateTime.UtcNow,
            IsBot = isBot,
        };
        await ChatMessages.AddAsync(chatMessage);
        await dataContext.SaveChangesAsync();
        
        return ChatsMapper.ToDomain(chatMessage);
    }

    public async Task<ICollection<Chat>> GetChats(Guid userId)
    {
        return Chats
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .AsEnumerable()
            .Select(ChatsMapper.ToDomain).
            ToArray();
    }

    public async Task<SearchChatMessagesResponse> SearchChatMessages(SearchChatMessagesRequest request)
    {
        var query = ChatMessages
            .AsNoTracking()
            .Where(e => e.ChatId == request.ChatId)
            .AsQueryable();

        return new SearchChatMessagesResponse(
            query.OrderByDescending(e => e.DateTime).Skip(request.Skip).Take(request.Take).AsEnumerable().Select(ChatsMapper.ToDomain).ToArray(),
            await query.CountAsync());
    }
}