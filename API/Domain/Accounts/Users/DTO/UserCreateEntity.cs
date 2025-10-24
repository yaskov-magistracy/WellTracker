namespace Domain.Accounts.Users.DTO;

public record UserCreateEntity(
    string Login,
    string HashedPassword,
    UserGender Gender,
    float Weight,
    int Height,
    UserTarget Target
);