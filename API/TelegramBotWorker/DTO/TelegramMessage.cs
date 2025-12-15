using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotWorker.DTO;

public record TelegramMessage(
long ChatId ,
string Text ,
ReplyMarkup? ReplyMarkup  = null,
ParseMode? ParseMode  = null)
{
    
}