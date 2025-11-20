using DAL.Chats;
using Domain.Chats;
using GigaChat;
using Infrastructure.Config;

namespace API.Modules.ChatsModule;

public class ChatsModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddSingleton<IGigaChatClient, GigaChatClient>(s =>
        {
            var config = (s.GetRequiredService<Config>()).GigaChat;
            return new GigaChatClient(config.AuthorizationKey, config.Scope);
        });

        services.AddScoped<IChatsRepository, ChatsRepository>();
        services.AddScoped<IChatsService, ChatsService>();
    }
}