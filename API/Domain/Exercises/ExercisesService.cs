using Domain.Exercises.DTO;
using Infrastructure.Results;

namespace Domain.Exercises;

public interface IExercisesService
{
    Task<Result<ExerciseSearchResponse>> Search(ExerciseSearchRequest request);
    Task<Result<Exercise>> GetById(Guid id);
    Task<EmptyResult> AddBatch(ICollection<ExerciseCreateEntity> request);
    Task<Result<Exercise>> Add(ExerciseCreateEntity request);
    Task<Result<Exercise>> Update(Guid id, ExerciseUpdateEntity request);
}

public class ExercisesService(
    IExercisesRepository exercisesRepository
) : IExercisesService
{
    public async Task<Result<ExerciseSearchResponse>> Search(ExerciseSearchRequest request)
    {
        var searchResponse = await exercisesRepository.Search(request);
        return Results.Ok(searchResponse);
    }

    public async Task<Result<Exercise>> GetById(Guid id)
    {
        var food = await exercisesRepository.Get(id);
        if (food == null)
            return Results.NotFound<Exercise>("");

        return Results.Ok(food);
    }

    public async Task<EmptyResult> AddBatch(ICollection<ExerciseCreateEntity> request)
    {
        await exercisesRepository.AddBatch(request);
        return EmptyResults.NoContent();
    }

    public async Task<Result<Exercise>> Add(ExerciseCreateEntity request)
    {
        var created = await exercisesRepository.Add(request);
        return Results.Ok(created);
    }

    public async Task<Result<Exercise>> Update(Guid id, ExerciseUpdateEntity request)
    {
        var exists = await exercisesRepository.Exists(id);
        if (!exists)
            return Results.NotFound<Exercise>("");
        
        var updated = await exercisesRepository.Update(id, request);
        return Results.Ok(updated);
    }
}