using System.ComponentModel.DataAnnotations;
using DAL.Chats;
using DAL.ExerciseDiaries;
using DAL.FoodDiaries;
using DAL.Statistics.Weight;

namespace DAL.Accounts.Users;

internal class UserEntity
{
    [Key] public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public UserGenderEntity Gender { get; set; }
    public float Weight { get; set; }
    public int Height { get; set; }
    public float TargetWeight { get; set; }
    public long? TgChatId { get; set; }
    
    public ICollection<FoodDiaryEntity> FoodDiaries { get; set; }
    public ICollection<ExerciseDiaryEntity> ExerciseDiaries { get; set; }
    public ICollection<WeightRecordEntity> WeightHistory { get; set; }
    public ICollection<ChatEntity> Chats { get; set; }
}

public enum UserGenderEntity
{
    Male,
    Female,
}