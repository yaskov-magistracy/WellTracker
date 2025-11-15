namespace Domain.Accounts.Users.DTO;

public record UserCreateRequest(
    string Login,
    string Password,
    UserGender Gender,
    float Weight,
    int Height,
    float TargetWeight
);