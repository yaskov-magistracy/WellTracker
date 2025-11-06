using System.ComponentModel.DataAnnotations;
using DAL.ExerciseDiaries;
using DAL.FoodDiaries;

namespace DAL.Accounts.Users;

internal class UserEntity
{
    [Key] public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public UserGenderEntity Gender { get; set; }
    public float Weight { get; set; }
    public int Height { get; set; }
    public UserTargetEntity Target { get; set; }
    
    public ICollection<FoodDiaryEntity> FoodDiaries { get; set; }
    public ICollection<ExerciseDiaryEntity> ExerciseDiaries { get; set; }
}

public enum UserGenderEntity
{
    Male,
    Female,
}

public enum UserTargetEntity
{
    LossWeight,
    KeepWeight,
    GainWeight,
}