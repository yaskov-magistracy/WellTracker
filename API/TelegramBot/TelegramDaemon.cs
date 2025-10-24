using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Commands;

namespace TelegramBot;

public class TelegramDaemon
{
    private readonly Dictionary<string, ITgCommand> textToCommands;
    
    public TelegramDaemon(
        TelegramBotClient client, 
        IEnumerable<ITgCommand> tgCommands)
    {
        textToCommands = tgCommands
            .SelectMany(command => command
                .GetCommands()
                .Select(key => new {key, command}))
            .ToDictionary(x => x.key, x => x.command);
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new[] 
            {
                UpdateType.Message, 
            }, 
            DropPendingUpdates = true,
        };
        
        using var cts = new CancellationTokenSource();
        client.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
    }
    
    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }

            if (!textToCommands.TryGetValue(update.Message!.Text!, out var tgCommand))
            {
                await client.SendMessage(update.Message.Chat.Id, "Неизвестная команда", replyMarkup: DefaultKeyboard);
                return;
            }
            
            var tgMessage = await tgCommand.HandleUpdate(update);
            if (tgMessage.ReplyMarkup == null)
                tgMessage = tgMessage with {ReplyMarkup = DefaultKeyboard};
            
            await client.SendMessage(
                tgMessage.ChatId,
                tgMessage.Text,
                replyMarkup: tgMessage.ReplyMarkup,
                parseMode: tgMessage.ParseMode ?? ParseMode.None 
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    
    private static ReplyKeyboardMarkup DefaultKeyboard = new(new List<KeyboardButton[]>()
    {
        new []
        {
            new KeyboardButton(TgNotificationBindingCommand.BindNotificationCommand)
                // {RequestContact = true},
        }
    })
    {
        ResizeKeyboard = true,
    };

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        var errorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}