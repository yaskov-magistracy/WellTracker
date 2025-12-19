using Domain.Database.Github;
using Domain.Exercises;
using Domain.Exercises.DTO;

namespace Domain.Database.Helpers;

public interface IDatabaseExercisesFiller
{
    Task FillFromRepo(
        int? maxEntities = null,
        int maxInChunk = 20);
}

public class DatabaseExercisesFiller(
    IExercisesService exercisesService,
    IGitHubFileReader gitHubFileReader
) : IDatabaseExercisesFiller
{
    public async Task FillFromRepo(
        int? maxEntities = null, 
        int maxInChunk = 20)
    {
        var (csvReader, exercises) = await gitHubFileReader.Read<ExerciseCreateEntity>("/ExercisesParser/exercises.csv");
        foreach (var exerciseBatch in exercises
                     .Take(maxEntities ?? int.MaxValue)
                     .Chunk(maxInChunk))
        {
            await exercisesService.AddBatch(exerciseBatch);
        }
        csvReader.Dispose();
    }
}