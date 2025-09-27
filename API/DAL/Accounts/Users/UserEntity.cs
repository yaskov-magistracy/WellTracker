using System.ComponentModel.DataAnnotations;

namespace DAL.Accounts.Users;

internal class UserEntity
{
    [Key] public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
}