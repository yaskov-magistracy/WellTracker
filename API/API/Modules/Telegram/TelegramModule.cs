using Infrastructure.Config;
using Telegram.Bot;
using TelegramBot;
using TelegramBot.Commands;

namespace API.Modules.Telegram;

public class TelegramModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddSingleton<TelegramBotClientSettings>(s => new(s.GetRequiredService<Config>().Telegram.Token));
        services.AddSingleton<TelegramBotClient>(s => new TelegramBotClient(s.GetRequiredService<Config>().Telegram.Token));
        services.AddSingleton<ITgCommand, TgDefaultCommand>();
        services.AddSingleton<ITgCommand, TgNotificationBindingCommand>();
        services.AddSingleton<TelegramDaemon>();
    }
}