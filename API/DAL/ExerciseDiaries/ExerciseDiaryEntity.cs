using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Accounts.Users;
using DAL.Exercises;

namespace DAL.ExerciseDiaries;

internal class ExerciseDiaryEntity
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey(nameof(User))] public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public DateOnly Date { get; set; }
    public ExerciseDiaryRecordEntity Current { get; set; }
}

internal class ExerciseDiaryRecordEntity
{
    public int Steps { get; set; }
    public ICollection<CompletedExerciseEntity>? CompletedExercises { get; set; }
    public ExerciseDiaryRecordInfoEntity Info { get; set; }
}

internal class CompletedExerciseEntity
{
    public Guid ExerciseId { get; set; }
    public int? Repeats { get; set; }
    public int? TimeInSeconds { get; set; }
}

internal class ExerciseDiaryRecordInfoEntity
{
    public float TotalKcalBurnt { get; set; }
}