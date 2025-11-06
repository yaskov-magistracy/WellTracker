using DAL.Exercises;
using Domain.ExerciseDiaries;
using Domain.Exercises;

namespace DAL.ExerciseDiaries;

internal static class ExerciseDiariesMapper
{
    public static ExerciseDiary ToDomain(
        Dictionary<Guid, ExerciseEntity> exercisesById,
        ExerciseDiaryEntity entity,
        ExerciseDiaryRecord target)
        => new(entity.Id,
            entity.UserId,
            entity.Date,
            ToDomain(exercisesById, entity.Current),
            target);

    public static ExerciseDiaryRecord ToDomain(
        Dictionary<Guid, ExerciseEntity> exercisesById, 
        ExerciseDiaryRecordEntity entity)
        => new(entity.Steps,
            entity.CompletedExercises?.Select(e => ToDomain(exercisesById, e)).ToArray(),
            ToDomain(entity.Info)
        );

    public static CompletedExercise ToDomain(
        Dictionary<Guid, ExerciseEntity> exercisesById,
        CompletedExerciseEntity entity)
        => new(
            ExercisesMapper.ToDomain(exercisesById[entity.ExerciseId]),
            entity.Repeats,
            entity.TimeInSeconds
        );

    public static ExerciseDiaryRecordInfo ToDomain(ExerciseDiaryRecordInfoEntity entity)
        => new(
            entity.TotalKcalBurnt
        );
}