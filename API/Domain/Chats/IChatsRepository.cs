using Domain.Chats.DTO;

namespace Domain.Chats;

public interface IChatsRepository
{
    Task<Chat> CreateChat(Guid userId, string title);
    Task<ChatMessage> SendMessage(Guid chatId, string message, bool isBot);
    Task<ICollection<Chat>> GetChats(Guid userId);
    Task<SearchChatMessagesResponse> SearchChatMessages(SearchChatMessagesRequest request);
}