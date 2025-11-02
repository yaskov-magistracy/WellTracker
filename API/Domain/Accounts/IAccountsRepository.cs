namespace Domain.Accounts;

public interface IAccountsRepository
{
    Task<Account> GetByLogin(string login);
    Task<bool> Exists(string login);
}