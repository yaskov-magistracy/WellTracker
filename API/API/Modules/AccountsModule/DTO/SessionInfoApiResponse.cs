using Domain.Accounts;

namespace API.Modules.AccountsModule.DTO;

public record class SessionInfoApiResponse(
    Guid UserId,
    AccountRole Role)
{
    
}