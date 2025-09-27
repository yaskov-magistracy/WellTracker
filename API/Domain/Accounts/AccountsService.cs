using System.Net;
using Domain.Accounts.Admins;
using Domain.Accounts.Admins.DTO;
using Domain.Accounts.DTO;
using Domain.Accounts.Users;
using Infrastructure.Results;

namespace Domain.Accounts;

public interface IAccountsService
{
    Task<Result<Account>> Login(AccountLoginRequest request);
    Task<Result<Account>> GetByLogin(string login);
    Task<Result<Account>> Add(AccountCreateRequest request);
    Task<EmptyResult> ChangePassword(AccountChangePasswordRequest request);
}

public class AccountsService(
    IAdminsService adminsService,
    IUsersService usersService
) : IAccountsService
{
    public async Task<Result<Account>> Login(AccountLoginRequest request)
    {
        var userResponse = await usersService.Login(request);
        if (userResponse.IsSuccess)
            return Results.Ok(ToAccount(userResponse.Value));
        
        var adminResponse = await adminsService.Login(request);
        if (adminResponse.IsSuccess)
            return Results.Ok(ToAccount(adminResponse.Value));
        
        return Results.BadRequest<Account>("Неправильный логин или пароль");
    }

    public async Task<Result<Account>> GetByLogin(string login)
    {
        var userResponse = await usersService.GetByLogin(login);
        if (userResponse.IsSuccess)
            return Results.Ok(ToAccount(userResponse.Value));
        
        var adminResponse = await adminsService.GetByLogin(login);
        if (adminResponse.IsSuccess)
            return Results.Ok(ToAccount(adminResponse.Value));

        return Results.NotFound<Account>("Аккаунта с таким логином не существует");
    }

    public async Task<Result<Account>> Add(AccountCreateRequest request)
    {
        var existedResponse = await GetByLogin(request.Login);
        if (existedResponse.StatusCode != HttpStatusCode.NotFound)
            return Results.BadRequest<Account>("Логин занят");

        if (request.Role == AccountRole.User)
        {
            var userResponse = await usersService.Register(new(request.Login, request.Password));
            if (!userResponse.IsSuccess)
                return Results.From<User, Account>(userResponse);
            
            return Results.Ok(ToAccount(userResponse.Value));
        }
        
        if (request.Role == AccountRole.Admin)
        {
            var adminResponse = await adminsService.Register((AdminCreateRequest) new(request.Login, request.Password));
            if (!adminResponse.IsSuccess)
                return Results.From<Admin, Account>(adminResponse);
            
            return Results.Ok(ToAccount(adminResponse.Value));
        }

        return Results.BadRequest<Account>($"Unsupported role: {request.Role}");
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
    
    private Account ToAccount(Admin admin)
        => new(admin.Id, AccountRole.Admin, admin.Login, admin.HashedPassword);

    private Account ToAccount(User user)
        => new(user.Id, AccountRole.User, user.Login, user.HashedPassword);
}