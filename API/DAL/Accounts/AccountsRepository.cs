using Domain.Accounts;

namespace DAL.Accounts;

public class AccountsRepository : IAccountsRepository
{
    public Task<AccountService> GetByLogin(string login)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exists(string login)
    {
        throw new NotImplementedException();
    }
}