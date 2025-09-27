using DAL.Accounts;
using DAL.Accounts.Admins;
using DAL.Accounts.Users;
using Domain.Accounts;
using Domain.Accounts.Admins;
using Domain.Accounts.Users;

namespace API.Modules.AccountsModule;

public class AccountsModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUsersService, UsersService>();
        
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IAdminsService, AdminsService>();
        
        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<IAccountsService, AccountsService>();
    }
}