using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Chats;

internal class ChatMessageEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Message { get; set; }
    public bool IsBot { get; set; }
    [ForeignKey(nameof(Chat))]public Guid ChatId { get; set; }
    public ChatEntity Chat { get; set; }
}