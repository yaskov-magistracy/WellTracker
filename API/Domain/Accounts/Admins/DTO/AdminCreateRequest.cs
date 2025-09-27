namespace Domain.Accounts.Admins.DTO;

public record class AdminCreateRequest(
    string Login,
    string Password
);