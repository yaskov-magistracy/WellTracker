namespace Domain.Accounts.Admins.DTO;

public record class AdminCreateEntity(
    string Login,
    string HashedPassword
);