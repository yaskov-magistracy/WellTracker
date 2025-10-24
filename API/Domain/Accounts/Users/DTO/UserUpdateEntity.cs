namespace Domain.Accounts.Users.DTO;

public class UserUpdateEntity
{
    public string? HashedPassword { get; set; }
    public UserGender? Gender {get; set; }
    public float? Weight {get; set; }
    public int? Height {get; set; }
    public UserTarget? Target {get; set; }
}