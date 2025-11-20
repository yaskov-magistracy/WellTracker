namespace Domain.ExerciseDiaries.DTO;

public record ExerciseDiaryUpdateEntity(
    int? Steps,
    ICollection<CompletedExerciseUpdateEntity>? Exercises)
{
    public IEnumerable<Guid> GetExercisesIds()
    {
        return Exercises == null 
            ? []
            : Exercises.Select(exercise => exercise.ExerciseId).Distinct();
    }
}