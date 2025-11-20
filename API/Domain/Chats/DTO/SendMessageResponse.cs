namespace Domain.Chats.DTO;

public record SendMessageResponse(
    ChatMessage Sent,
    ChatMessage Received)
{
}