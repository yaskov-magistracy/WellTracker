using Telegram.Bot.Types;
using TelegramBotWorker.DTO;

namespace TelegramBotWorker.Commands;

public interface ITgCommand
{
    string[] GetCommands();
    Task<TelegramMessage> HandleUpdate(Update update);
}