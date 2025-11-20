using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Accounts.Users;

namespace DAL.Statistics.Weight;

internal class WeightRecordEntity
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey(nameof(User))] public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public float Weight { get; set; }
    public DateOnly Date { get; set; }
}