using Domain.Accounts.Users;
using Domain.Accounts.Users.DTO;

namespace DAL.Accounts.Users;

internal static class UsersMapper
{
    public static User ToDomain(UserEntity entity)
        => new(
            entity.Id,
            entity.Login,
            entity.HashedPassword,
            ToDomain(entity.Gender),
            entity.Weight,
            entity.Height,
            entity.TargetWeight
        );

    private static UserGender ToDomain(UserGenderEntity entity) => entity switch
    {
        UserGenderEntity.Male => UserGender.Male,
        UserGenderEntity.Female => UserGender.Female,
        _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null),
    };

    public static UserEntity ToEntity(UserCreateEntity createEntity)
        => new()
        {
            Login = createEntity.Login,
            HashedPassword = createEntity.HashedPassword,
            Gender = ToEntity(createEntity.Gender),
            Weight = createEntity.Weight,
            Height = createEntity.Height,
            TargetWeight = createEntity.TargetWeight,
        };

    public static UserGenderEntity ToEntity(UserGender createEntity) => createEntity switch
    {
        UserGender.Male => UserGenderEntity.Male,
        UserGender.Female => UserGenderEntity.Female,
        _ => throw new ArgumentOutOfRangeException(nameof(createEntity), createEntity, null),
    };
}