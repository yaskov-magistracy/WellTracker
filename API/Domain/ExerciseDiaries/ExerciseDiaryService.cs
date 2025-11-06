using Domain.ExerciseDiaries.DTO;
using Domain.Exercises;
using Infrastructure.Results;

namespace Domain.ExerciseDiaries;

public interface IExerciseDiariesService
{
    Task<Result<ExerciseDiary>> GetByDate(Guid userId, DateOnly date);
    Task<Result<ExerciseDiary>> CreateOrUpdate(Guid userId, DateOnly date, ExerciseDiaryUpdateEntity updateEntity);
}

public class ExerciseDiaryService(
    IExerciseDiariesRepository exerciseDiariesRepository,
    IExercisesRepository exercisesRepository
) : IExerciseDiariesService
{
    public async Task<Result<ExerciseDiary>> GetByDate(Guid userId, DateOnly date)
    {
        var exerciseDiary = await exerciseDiariesRepository.GetByDate(userId, date);
        if (exerciseDiary == null)
            return Results.NotFound<ExerciseDiary>("");
        
        return Results.Ok(exerciseDiary);
    }

    public async Task<Result<ExerciseDiary>> CreateOrUpdate(Guid userId, DateOnly date, ExerciseDiaryUpdateEntity updateEntity)
    {
        if (updateEntity.Exercises?.Any(e => e.Repeats == null && e.TimeInSeconds == null) is true)
            return Results.BadRequest<ExerciseDiary>("Упражнение должно в чём-то измеряться");
        
        foreach (var exerciseId in updateEntity.GetExercisesIds())
            if (!(await exercisesRepository.Exists(exerciseId)))
                return Results.NotFound<ExerciseDiary>($"Упражнение не найдено. Id: {exerciseId}");
        
        var updated = await exerciseDiariesRepository.CreateOrUpdate(userId, date, updateEntity);
        return Results.Ok(updated);
    }
}