using DAL.Accounts.Users;
using Domain.Accounts.Users;

namespace API.Modules.UsersModule;

public class UsersModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUsersService, UsersService>();
    }
}