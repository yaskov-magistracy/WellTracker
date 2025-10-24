using Domain.Accounts.Users;
using Domain.Accounts.Users.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Accounts.Users;

public class UsersRepository(
    DataContext dataContext
) : IUsersRepository
{
    private DbSet<UserEntity> Users => dataContext.Users;

    public async Task<User?> Get(Guid id)
    {
        var entity = await Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity != null
            ? UsersMapper.ToDomain(entity)
            : null;
    }
    
    public async Task<User?> GetByLogin(string login)
    {
        var entity = await Users.AsNoTracking().FirstOrDefaultAsync(e => e.Login == login);
        return entity != null
            ? UsersMapper.ToDomain(entity)
            : null;
    }

    public async Task<bool> Exists(string login)
    {
        var entity = await Users.AsNoTracking().FirstOrDefaultAsync(e => e.Login == login);
        return entity != null;
    }

    public async Task<User> Add(UserCreateEntity createEntity)
    {
        var entity = UsersMapper.ToEntity(createEntity);
        await Users.AddAsync(entity);
        await dataContext.SaveChangesAsync();
        return UsersMapper.ToDomain(entity);
    }

    public async Task<User> Update(Guid userId, UserUpdateEntity updateEntity)
    {
        var user = await Users.FirstAsync(e => e.Id == userId);
        if (updateEntity.HashedPassword != null)
            user.HashedPassword = updateEntity.HashedPassword;
        if (updateEntity.Gender != null)
            user.Gender = UsersMapper.ToEntity(updateEntity.Gender.Value);
        if (updateEntity.Weight != null)
            user.Weight = updateEntity.Weight.Value;
        if (updateEntity.Height != null)
            user.Height = updateEntity.Height.Value;
        if (updateEntity.Target != null)
            user.Target = UsersMapper.ToEntity(updateEntity.Target.Value);
        
        await dataContext.SaveChangesAsync();
        return UsersMapper.ToDomain(user);
    }
}