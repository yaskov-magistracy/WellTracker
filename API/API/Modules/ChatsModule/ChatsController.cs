using API.Configuration.Auth;
using API.Modules.ChatsModule.DTO;
using Domain.Advices;
using Domain.Advices.DTO;
using Domain.Chats;
using Domain.Chats.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Chat = Domain.Chats.Chat;

namespace API.Modules.ChatsModule;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ChatsController(
    IChatsService chatsService,
    IReportsService reportsService
) : ControllerBase
{
    /// <summary>
    /// Получить все свои чаты
    /// </summary>
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
        var response = await chatsService.SendMessage(chatId, User.GetId(), request.Message);
        return response.ActionResult;
    }

    /// <summary>
    /// Получить отчёт за неделю/вчера
    /// </summary>
    [HttpPost("{chatId:Guid}/report")]
    public async Task<ActionResult<SendMessageResponse>> GetReport(
        [FromRoute] Guid chatId,
        [FromQuery] bool isWeekly = false)
    {
        var userId = User.GetId();
        Report report;
        string period;
        if (isWeekly)
        {
            period = "неделю";
            report = (await reportsService.GetWeeklyReport(userId, DateOnly.FromDateTime(DateTime.UtcNow))).Value;
        }
        else
        {
            period = "вчера";
            report = (await reportsService.GetDailyReport(userId, DateOnly.FromDateTime(DateTime.UtcNow))).Value;
        }
        var userMessage = $"Проанализируй мои результаты за {period}";
        var sentMessage = await chatsService.SendDeterminedMessage(chatId, userMessage, report.Text);
        return sentMessage.ActionResult;
    }
}