using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.DTO;

public record TelegramMessage(
long ChatId ,
string Text ,
ReplyMarkup? ReplyMarkup  = null,
ParseMode? ParseMode  = null)
{
    
}