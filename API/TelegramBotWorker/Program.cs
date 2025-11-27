using Infrastructure.Config;
using Telegram.Bot;
using TelegramBotWorker;
using TelegramBotWorker.Commands;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<Config>();
builder.Services.AddSingleton<TelegramBotClientSettings>(s => new(s.GetRequiredService<Config>().Telegram.Token));
builder.Services.AddSingleton<TelegramBotClient>(s => new TelegramBotClient(s.GetRequiredService<Config>().Telegram.Token));
RegisterTgCommands(builder.Services);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

void RegisterTgCommands(IServiceCollection services)
{
    services.AddSingleton<ITgCommand, TgDefaultCommand>();
    services.AddSingleton<ITgCommand, TgNotificationBindingCommand>();
}