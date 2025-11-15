namespace Domain.Accounts.Users;

public record User(
    Guid Id,
    string Login,
    string HashedPassword,
    UserGender Gender,
    float Weight,
    int Height,
    float TargetWeight
);

public enum UserGender
{
    Male,
    Female,
}