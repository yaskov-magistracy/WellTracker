using Infrastructure.Config;
using Telegram.Bot;
using TelegramBotWorker;
using TelegramBotWorker.Commands;

var builder = Host.CreateApplicationBuilder(args);
DAL.Dependencies.Register(builder.Services);
RegisterTgCommands(builder.Services);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

void RegisterTgCommands(IServiceCollection services)
{
    services.AddSingleton<ITgCommand, TgDefaultCommand>();
    services.AddSingleton<ITgCommand, TgNotificationBindingCommand>();
}