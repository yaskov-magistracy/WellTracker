using System.Net;
using Domain.Accounts.Admins;
using Domain.Accounts.Admins.DTO;
using Domain.Accounts.DTO;
using Domain.Accounts.Users;
using Infrastructure.Results;

namespace Domain.Accounts;

public interface IAccountsService
{
    Task<Result<AccountService>> Login(AccountLoginRequest request);
    Task<Result<AccountService>> GetByLogin(string login);
    Task<EmptyResult> ChangePassword(AccountChangePasswordRequest request);
}

public class AccountsService(
    IAdminsService adminsService,
    IUsersService usersService
) : IAccountsService
{
    public async Task<Result<AccountService>> Login(AccountLoginRequest request)
    {
        var userResponse = await usersService.Login(request);
        if (userResponse.IsSuccess)
            return Results.Ok(ToAccount(userResponse.Value));
        
        var adminResponse = await adminsService.Login(request);
        if (adminResponse.IsSuccess)
            return Results.Ok(ToAccount(adminResponse.Value));
        
        return Results.BadRequest<AccountService>("Неправильный логин или пароль");
    }

    public async Task<Result<AccountService>> GetByLogin(string login)
    {
        var userResponse = await usersService.GetByLogin(login);
        if (userResponse.IsSuccess)
            return Results.Ok(ToAccount(userResponse.Value));
        
        var adminResponse = await adminsService.GetByLogin(login);
        if (adminResponse.IsSuccess)
            return Results.Ok(ToAccount(adminResponse.Value));

        return Results.NotFound<AccountService>("Аккаунта с таким логином не существует");
    }

    public async Task<EmptyResult> ChangePassword(AccountChangePasswordRequest request)
    {
        var usersResponse = await usersService.ChangePassword(request);
        if (usersResponse.IsSuccess)
            return EmptyResults.NoContent();
        
        var adminsResponse = await usersService.ChangePassword(request);
        if (adminsResponse.IsSuccess)
            return EmptyResults.NoContent();

        return EmptyResults.BadRequest("Неправильный логин или пароль");
    }
    
    private AccountService ToAccount(Admin admin)
        => new(admin.Id, AccountRole.Admin, admin.Login, admin.HashedPassword);

    private AccountService ToAccount(User user)
        => new(user.Id, AccountRole.User, user.Login, user.HashedPassword);
}