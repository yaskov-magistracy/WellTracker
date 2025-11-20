using Infrastructure.DTO.Search;

namespace Domain.Chats.DTO;

public record SearchChatMessagesRequest(
    Guid ChatId,
    int Take = 15,
    int Skip = 0) : BaseSearchRequest<ChatMessage>(Take, Skip)
{
    
}