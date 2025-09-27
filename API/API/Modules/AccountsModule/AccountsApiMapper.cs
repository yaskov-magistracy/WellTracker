using API.Modules.AccountsModule.DTO;
using Domain.Accounts.DTO;

namespace API.Modules.AccountsModule;

public static class AccountsApiMapper
{
    public static AccountChangePasswordRequest ToDomain(Guid userId, AccountChangePasswordApiRequest apiRequest) 
        => new(userId, apiRequest.OldPassword, apiRequest.NewPassword);
}