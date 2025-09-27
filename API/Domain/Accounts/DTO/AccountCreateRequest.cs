namespace Domain.Accounts.DTO;

public record class AccountCreateRequest(
    string Login,
    string Password,
    AccountRole Role
);