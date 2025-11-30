using Domain.Accounts.Users.DTO;

namespace Domain.Accounts.Users;

public interface IUsersRepository
{
    Task<User?> Get(Guid id);
    Task<ICollection<User>> GetAll();
    Task<User?> GetByLogin(string login);
    Task<bool> Exists(string login);
    Task<User> Add(UserCreateEntity createEntity);
    Task<User> Update(Guid userId, UserUpdateEntity updateEntity);
}