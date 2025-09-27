namespace Domain.Accounts.Admins;

public record class Admin(
    Guid Id,
    string Login,
    string HashedPassword
);