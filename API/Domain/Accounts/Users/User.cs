namespace Domain.Accounts.Users;

public record User(
    Guid Id,
    string Login,
    string HashedPassword,
    UserGender Gender,
    float Weight,
    int Height,
    UserTarget Target
);

public enum UserGender
{
    Male,
    Female,
}

public enum UserTarget
{
    LossWeight,
    KeepWeight,
    GainWeight,
}