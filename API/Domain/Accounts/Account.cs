namespace Domain.Accounts;

public record class Account(
    Guid Id,
    AccountRole Role,
    string Login,
    string HashedPassword)
{
}