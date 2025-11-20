using Domain.Exercises;

namespace Domain.Database.Helpers;

public interface IDatabaseExercisesFiller
{
    Task FillFromRepo(
        int? maxEntities = null,
        int maxInChunk = 20);
}

public class DatabaseExercisesFiller(
    IExercisesService exercisesService
) : IDatabaseExercisesFiller
{
    public async Task FillFromRepo(int? maxEntities = null, int maxInChunk = 20)
    {
        await exercisesService.AddBatch([
            new("Жим штанги лёжа", 
                "Описание",
                ExerciseType.Strength, 
                ExerciseMeasurement.Repeats,
                [MuscleType.Chest, MuscleType.Triceps],
                [EquipmentType.Barbell, EquipmentType.Bench])
        ]);
    }
}