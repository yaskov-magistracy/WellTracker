using Domain.ExerciseDiaries.DTO;

namespace Domain.ExerciseDiaries;

public interface IExerciseDiariesRepository
{
    Task<ExerciseDiary?> GetByDate(Guid userId, DateOnly date);
    Task<bool> Exists(Guid userId, DateOnly date);
    Task<ExerciseDiary> CreateOrUpdate(Guid userId, DateOnly date, ExerciseDiaryUpdateEntity updateEntity);
}