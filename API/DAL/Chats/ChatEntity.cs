using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Accounts.Users;

namespace DAL.Chats;

internal class ChatEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    [ForeignKey(nameof(User))] public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public ICollection<ChatMessageEntity> Messages { get; set; }
}