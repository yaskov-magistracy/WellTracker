namespace Domain.Accounts;

public record class AccountService(
    Guid Id,
    AccountRole Role,
    string Login,
    string HashedPassword)
{
}