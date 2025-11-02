namespace Domain.Accounts;

public interface IAccountsRepository
{
    Task<AccountService> GetByLogin(string login);
    Task<bool> Exists(string login);
}