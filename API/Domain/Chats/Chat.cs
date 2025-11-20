namespace Domain.Chats;

public record Chat(
    Guid Id,
    string Title,
    Guid UserId)
{
    
}