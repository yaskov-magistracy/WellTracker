using Infrastructure.DTO.Search;

namespace Domain.Chats.DTO;

public record SearchChatMessagesResponse(
    ICollection<ChatMessage> Items,
    int TotalCount
) : BaseSearchResponse<ChatMessage>(Items, TotalCount)
{
    
}