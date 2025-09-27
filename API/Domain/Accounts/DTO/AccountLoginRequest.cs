namespace Domain.Accounts.DTO;

public record class AccountLoginRequest(
    string Login, 
    string Password
);