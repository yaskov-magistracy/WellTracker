using Domain.Accounts.Users;
using Infrastructure.DTO;

namespace API.Modules.UsersModule.DTO;

public class UserInfoPatchRequest
{
    public UserGender? Gender {get; set; }
    public float? Weight {get; set; }
    public int? Height {get; set; }
    public float? TargetWeight {get; set; }
    public NullablePatch<long?> TgChatId { get; set; }
}