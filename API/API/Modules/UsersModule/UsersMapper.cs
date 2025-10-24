using API.Modules.UsersModule.DTO;
using Domain.Accounts.Users;
using Domain.Accounts.Users.DTO;

namespace API.Modules.UsersModule;

internal static class UsersMapper
{
    public static UserInfoApiResponse ToApiResponse(User user)
        => new(user.Gender, user.Weight, user.Height, user.Target);

    public static UserUpdateEntity ToDomain(UserInfoPatchRequest request)
        => new()
        {
            Gender = request.Gender,
            Weight = request.Weight,
            Height = request.Height,
            Target = request.Target,
        };
}