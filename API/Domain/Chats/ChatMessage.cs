namespace Domain.Chats;

public record ChatMessage(
    Guid Id,
    DateTime DateTime,
    string Message,
    bool IsBot)
{
}