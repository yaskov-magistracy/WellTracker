namespace Domain.Accounts.Users.DTO;

public record class UserCreateRequest(
    string Login,
    string Password
);