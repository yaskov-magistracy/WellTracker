using Domain.Accounts.Users;

namespace API.Modules.UsersModule.DTO;

public class UserInfoPatchRequest
{
    public UserGender? Gender {get; set; }
    public float? Weight {get; set; }
    public int? Height {get; set; }
    public UserTarget? Target {get; set; }
}