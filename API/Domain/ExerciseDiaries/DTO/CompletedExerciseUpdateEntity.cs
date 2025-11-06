namespace Domain.ExerciseDiaries.DTO;

public record CompletedExerciseUpdateEntity(
    Guid ExerciseId,
    int? Repeats,
    int? TimeInSeconds
);