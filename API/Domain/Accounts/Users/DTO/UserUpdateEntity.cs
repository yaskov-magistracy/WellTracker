using Infrastructure.DTO;

namespace Domain.Accounts.Users.DTO;

public class UserUpdateEntity
{
    public string? HashedPassword { get; set; }
    public UserGender? Gender {get; set; }
    public float? Weight {get; set; }
    public int? Height {get; set; }
    public float? TargetWeight {get; set; }
    public NullablePatch<long?>? TgChatId { get; set; }
}