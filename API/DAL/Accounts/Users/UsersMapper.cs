using Domain.Accounts.Users;
using Domain.Accounts.Users.DTO;

namespace DAL.Accounts.Users;

internal static class UsersMapper
{
    public static User ToDomain(UserEntity entity) 
        => new(entity.Id, entity.Login, entity.HashedPassword);
    
    public static UserEntity ToEntity(UserCreateEntity createEntity) 
        => new()
        {
            Login = createEntity.Login,
            HashedPassword = createEntity.HashedPassword,
        };
}