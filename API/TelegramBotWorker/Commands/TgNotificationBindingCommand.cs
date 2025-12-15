using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotWorker.DTO;

namespace TelegramBotWorker.Commands;

public class TgNotificationBindingCommand(
) : ITgCommand
{
    public const string BindNotificationCommand = "Привязать уведомления";
    public static readonly string[] Commands = [BindNotificationCommand];
    public string[] GetCommands() => Commands;

    public async Task<TelegramMessage> HandleUpdate(Update update)
    {
        var message = update.Message!;
        var (text, chat) = (message.Text, message.Chat);

        if (text == BindNotificationCommand)
        {
            return new(
                chat.Id,
                @$"Ваш Id чата: `{chat.Id}`\. Укажите его в настройках аккаунта, чтобы получать уведомления здесь",
                ParseMode: ParseMode.MarkdownV2);
        }

        throw new ArgumentException();
    }
}

public record Test(int a, int? b = null, int? c = null);