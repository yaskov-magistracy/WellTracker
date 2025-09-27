namespace API.Modules.AccountsModule.DTO;

public record class AccountChangePasswordApiRequest(
    string OldPassword,
    string NewPassword
);