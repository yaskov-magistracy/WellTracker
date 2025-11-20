using API.Configuration.Auth;
using API.Modules.ChatsModule.DTO;
using Domain.Chats;
using Domain.Chats.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.ChatsModule;

[Route("api/[controller]")]
[ApiController]
public class ChatsController(
    IChatsService chatsService    
) : ControllerBase
{
    /// <summary>
    /// Получить все свои чаты
    /// </summary>
    [Authorize]
    [HttpGet("")]
    public async Task<ActionResult<ICollection<Chat>>> GetChats()
    {
        var response = await chatsService.GetChats(User.GetId());
        return response.ActionResult;
    }
    
    /// <summary>
    /// Получить сообщения в чате
    /// </summary>
    [HttpGet("{chatId:Guid}")]
    public async Task<ActionResult<SearchChatMessagesResponse>> GetMessagesInChat(
        [FromRoute] Guid chatId,
        [FromQuery] int take = 15,
        [FromQuery] int skip = 0)
    {
        var response = await chatsService.SearchChatMessages(
            new(chatId, take, skip));
        
        return response.ActionResult;
    }

    /// <summary>
    /// Создание нового чата.
    /// </summary>
    /// <remarks>
    /// Создавай чат перед тем как отправить первое сообщение <br/>
    /// `Title` - название чата. Обычно туда просто ставят начало первого сообщения. Ограничение - 20 символов
    /// </remarks>
    [Authorize]
    [HttpPost("")]
    public async Task<ActionResult<Chat>> CreateChat([FromBody] CreateChatRequest request)
    {
        var response = await chatsService.CreateChat(User.GetId(), request.Title);
        return response.ActionResult;
    }

    /// <summary>
    /// Отправить сообщение в чат
    /// </summary>
    /// <remarks>
    /// Возвращает написанное сообщение и то что ответил Бот
    /// </remarks>
    [HttpPost("{chatId:Guid}/messages")]
    public async Task<ActionResult<SendMessageResponse>> SendMessage(
        [FromRoute] Guid chatId,
        [FromBody] SendMessageRequest request)
    {
        var response = await chatsService.SendMessage(chatId, request.Message);
        return response.ActionResult;
    }
}