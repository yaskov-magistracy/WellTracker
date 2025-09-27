using Domain.Accounts.Admins.DTO;

namespace Domain.Accounts.Admins;

public interface IAdminsRepository
{
    Task<Admin?> Get(Guid id);
    Task<Admin?> GetByLogin(string login);
    Task<bool> Exists(string login);
    Task<Admin> Add(AdminCreateEntity createEntity);
    Task<Admin> Update(Guid adminId, AdminUpdateEntity updateEntity);
}