namespace Domain.Accounts.Users.DTO;

public record class UserCreateEntity(
    string Login,
    string HashedPassword)
{
}