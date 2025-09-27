namespace Domain.Accounts.DTO;

public record class AccountChangePasswordRequest(
    Guid UserId,
    string OldPassword,
    string NewPassword
);