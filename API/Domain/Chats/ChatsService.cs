using Domain.Chats.DTO;
using GigaChat;
using GigaChat.Completions.Request;
using Infrastructure.Results;

namespace Domain.Chats;

public interface IChatsService
{
    Task<Result<ICollection<Chat>>> GetChats(Guid userId);
    Task<Result<Chat>> CreateChat(Guid userId, string message);
    Task<Result<SendMessageResponse>> SendMessage(Guid chatId, Guid userId, string message);
    Task<Result<SendMessageResponse>> SendDeterminedMessage(Guid chatId, string userMessage, string botMessage);
    Task<Result<SearchChatMessagesResponse>> SearchChatMessages(SearchChatMessagesRequest request);
}

public class ChatsService(
    IChatsRepository chatsRepository,
    IGigaChatClient gigaChatClient,
    IChatContextProvider chatContextProvider
) : IChatsService
{
    public async Task<Result<Chat>> CreateChat(Guid userId, string message)
    {
        var chat = await chatsRepository.CreateChat(userId, message.Substring(0, Math.Min(message.Length, 20)));
        return Results.Ok(chat);
    }

    public async Task<Result<ICollection<Chat>>> GetChats(Guid userId)
    {
        var chats = await chatsRepository.GetChats(userId);
        return Results.Ok(chats);
    }

    public async Task<Result<SearchChatMessagesResponse>> SearchChatMessages(SearchChatMessagesRequest request)
    {
        var response = await chatsRepository.SearchChatMessages(request);
        return Results.Ok(response);
    }
    
    public async Task<Result<SendMessageResponse>> SendMessage(Guid chatId, Guid userId, string message)
    {
        var context = await chatContextProvider.GetChatContext(chatId, userId);
        context.Add(new GigaChatCompletionsRequestMessage()
        {
            Role = GigaChatCompletionsRequestMessageRole.User,
            Content = message
        });
        var gigaChatResponse = await gigaChatClient.Completions(new GigaChatCompletionsRequest()
        {
            Messages = context.ToArray()
        });
        var sentMessage = await chatsRepository.SendMessage(chatId, message, false);
        var botMessage = await chatsRepository.SendMessage(
            chatId,
            gigaChatResponse.Choices.First().Message.Content,
            true);
        
        return Results.Ok(new SendMessageResponse(sentMessage, botMessage));
    }
    
    public async Task<Result<SendMessageResponse>> SendDeterminedMessage(Guid chatId, string userMessage, string botMessage)
    {
        var userSentMessage = await chatsRepository.SendMessage(chatId, userMessage, false);
        var botSentMessage = await chatsRepository.SendMessage(chatId, botMessage, true);
        
        return Results.Ok(new SendMessageResponse(userSentMessage, botSentMessage));
    }
}