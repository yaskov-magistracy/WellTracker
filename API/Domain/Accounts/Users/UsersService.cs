using Domain.Accounts.DTO;
using Domain.Accounts.Users.DTO;
using Infrastructure.Results;

namespace Domain.Accounts.Users;

public interface IUsersService
{
    Task<Result<User>> Login(AccountLoginRequest request);
    Task<Result<User>> GetByLogin(string login);
    Task<Result<User>> Register(UserCreateRequest request);
    Task<EmptyResult> ChangePassword(AccountChangePasswordRequest request);
}

public class UsersService(
    IUsersRepository usersRepository,
    IPasswordHasher passwordHasher
) : IUsersService
{
    public async Task<Result<User>> Login(AccountLoginRequest request)
    {
        var user = await usersRepository.GetByLogin(request.Login);
        if (user == null)
            return Results.BadRequest<User>("Неправильный логин или пароль");
        var isPasswordCorrect = passwordHasher.VerifyPassword(request.Password, user.HashedPassword);
        if (!isPasswordCorrect)
            return Results.BadRequest<User>("Неправильный логин или пароль");

        return Results.Ok(user);
    }
    
    public async Task<Result<User>> GetByLogin(string login)
    {
        var user = await usersRepository.GetByLogin(login);
        if (user == null)
            return Results.NotFound<User>("Такого Пользователя не существует");

        return Results.Ok(user);
    }

    public async Task<Result<User>> Register(UserCreateRequest request)
    {
        var existed = await usersRepository.GetByLogin(request.Login);
        if (existed != null)
            return Results.BadRequest<User>("Пользователь с таким логином уже существует");

        var newUser = await usersRepository.Add(new(
            request.Login,
            passwordHasher.HashPassword(request.Password)));
        return Results.Ok(newUser);
    }

    public async Task<EmptyResult> ChangePassword(AccountChangePasswordRequest request)
    {
        var existed = await usersRepository.Get(request.UserId);
        if (existed == null)
            return EmptyResults.NotFound("Пользователя с таким логином не существует");
        var isPasswordCorrect = passwordHasher.VerifyPassword(request.OldPassword, existed.HashedPassword);
        if (!isPasswordCorrect)
            return EmptyResults.BadRequest("Неправильный старый пароль");

        await usersRepository.Update(
            existed.Id,
            new()
            {
                HashedPassword = passwordHasher.HashPassword(request.NewPassword)
            });
        return EmptyResults.NoContent();
    }
}