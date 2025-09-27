using Domain.Accounts.Admins;
using Domain.Accounts.Admins.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Accounts.Admins;

public class AdminsRepository(
    DataContext dataContext    
) : IAdminsRepository
{
    private DbSet<AdminEntity> Admins => dataContext.Admins;

    public async Task<Admin?> Get(Guid id)
    {
        var entity = await Admins.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity != null
            ? AdminsMapper.ToDomain(entity)
            : null;
    }

    public async Task<Admin?> GetByLogin(string login)
    {
        var entity = await Admins.AsNoTracking().FirstOrDefaultAsync(e => e.Login == login);
        return entity != null
            ? AdminsMapper.ToDomain(entity)
            : null;
    }

    public async Task<bool> Exists(string login)
    {
        var entity = await Admins.AsNoTracking().FirstOrDefaultAsync(e => e.Login == login);
        return entity != null;
    }

    public async Task<Admin> Add(AdminCreateEntity createEntity)
    {
        var entity = AdminsMapper.ToEntity(createEntity);
        await Admins.AddAsync(entity);
        await dataContext.SaveChangesAsync();
        return AdminsMapper.ToDomain(entity);
    }

    public async Task<Admin> Update(Guid adminId, AdminUpdateEntity updateEntity)
    {
        var admin = await Admins.FirstAsync(e => e.Id == adminId);
        if (updateEntity.HashedPassword != null)
            admin.HashedPassword = updateEntity.HashedPassword;
        
        await dataContext.SaveChangesAsync();
        return AdminsMapper.ToDomain(admin);
    }
}