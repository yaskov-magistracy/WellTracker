namespace Domain.Accounts.Users;

public record class User(
    Guid Id,
    string Login,
    string HashedPassword
);