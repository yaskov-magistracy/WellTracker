using Telegram.Bot.Types;
using TelegramBotWorker.DTO;

namespace TelegramBotWorker.Commands;

public class TgDefaultCommand(
) : ITgCommand
{
    private const string DefaultCommand = "/start";
    private static readonly string[] Commands = [DefaultCommand];
    public string[] GetCommands() => Commands;

    public async Task<TelegramMessage> HandleUpdate(Update update)
    {
        var message = update.Message!;
        var (text, chat) = (message.Text, message.Chat);
        
        if (text == DefaultCommand)
        {
            return new(
                chat.Id,
                @"Это бот команды Welltracker. Здесь можно получать уведомления и взаимодействовать с системой"
            );
        }

        throw new ArgumentException();
    }
}