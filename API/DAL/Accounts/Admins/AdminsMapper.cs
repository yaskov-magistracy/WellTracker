using Domain.Accounts.Admins;
using Domain.Accounts.Admins.DTO;

namespace DAL.Accounts.Admins;

internal static class AdminsMapper
{
    public static Admin ToDomain(AdminEntity entity) 
        => new(entity.Id, entity.Login, entity.HashedPassword);
    
    public static AdminEntity ToEntity(AdminCreateEntity createEntity) 
        => new()
        {
            Login = createEntity.Login,
            HashedPassword = createEntity.HashedPassword,
        };
}