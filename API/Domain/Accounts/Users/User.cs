namespace Domain.Accounts.Users;

public record User(
    Guid Id,
    string Login,
    string HashedPassword,
    UserGender Gender,
    float Weight,
    int Height,
    float TargetWeight
)
{
    public UserTargetType GetTargetType()
    {
        if (TargetWeight > Weight)
            return UserTargetType.Gain;
        if (TargetWeight < Weight)
            return UserTargetType.Lose;

        return UserTargetType.Keep;
    }
}

public enum UserGender
{
    Male,
    Female,
}

public enum UserTargetType
{
    Gain,
    Keep,
    Lose,
}