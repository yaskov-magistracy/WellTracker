using Domain.Exercises.DTO;

namespace Domain.Exercises;

public interface IExercisesRepository
{
    Task<ExerciseSearchResponse> Search(ExerciseSearchRequest request);
    Task<Exercise?> Get(Guid exerciseId);
    Task<bool> Exists(Guid exerciseId);
    Task AddBatch(ICollection<ExerciseCreateEntity> createEntities);
    Task<Exercise> Add(ExerciseCreateEntity createEntity);
    Task<Exercise> Update(Guid exerciseId, ExerciseUpdateEntity updateEntity);
}