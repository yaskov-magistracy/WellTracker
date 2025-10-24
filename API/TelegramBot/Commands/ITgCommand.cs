using Telegram.Bot.Types;
using TelegramBot.DTO;

namespace TelegramBot.Commands;

public interface ITgCommand
{
    string[] GetCommands();
    Task<TelegramMessage> HandleUpdate(Update update);
}