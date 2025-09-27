using Domain.Accounts.Admins.DTO;
using Domain.Accounts.DTO;
using Infrastructure.Results;

namespace Domain.Accounts.Admins;

public interface IAdminsService
{
    Task<Result<Admin>> Login(AccountLoginRequest request);
    Task<Result<Admin>> GetByLogin(string login);
    Task<Result<Admin>> Register(AdminCreateRequest request);
    Task<EmptyResult> ChangePassword(AccountChangePasswordRequest request);
}

public class AdminsService(
    IAdminsRepository adminsRepository,
    IPasswordHasher passwordHasher
) : IAdminsService
{
    public async Task<Result<Admin>> Login(AccountLoginRequest request)
    {
        var admin = await adminsRepository.GetByLogin(request.Login);
        if (admin == null)
            return Results.BadRequest<Admin>("Неправильный логин или пароль");
        var isPasswordCorrect = passwordHasher.VerifyPassword(request.Password, admin.HashedPassword);
        if (!isPasswordCorrect)
            return Results.BadRequest<Admin>("Неправильный логин или пароль");

        return Results.Ok(admin);
    }

    public async Task<Result<Admin>> GetByLogin(string login)
    {
        var admin = await adminsRepository.GetByLogin(login);
        if (admin == null)
            return Results.NotFound<Admin>("Такого Админа не существует");

        return Results.Ok(admin);
    }

    public async Task<Result<Admin>> Register(AdminCreateRequest request)
    {
        var existed = await adminsRepository.GetByLogin(request.Login);
        if (existed != null)
            return Results.BadRequest<Admin>("Админ с таким логином уже существует");

        var newAdmin = await adminsRepository.Add(new(
            request.Login,
            passwordHasher.HashPassword(request.Password)));
        return Results.Ok(newAdmin);
    }

    public async Task<EmptyResult> ChangePassword(AccountChangePasswordRequest request)
    {
        var existed = await adminsRepository.Get(request.UserId);
        if (existed == null)
            return EmptyResults.NotFound("Админа с таким логином не существует");
        var isPasswordCorrect = passwordHasher.VerifyPassword(request.OldPassword, existed.HashedPassword);
        if (!isPasswordCorrect)
            return EmptyResults.BadRequest("Неправильный старый пароль");

        await adminsRepository.Update(
            existed.Id,
            new()
            {
                HashedPassword = passwordHasher.HashPassword(request.NewPassword)
            });
        return EmptyResults.NoContent();
    }
}