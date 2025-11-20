using Domain.Exercises;

namespace Domain.ExerciseDiaries;

public record ExerciseDiary(
    Guid Id,
    Guid UserId,
    DateOnly Date,
    ExerciseDiaryRecord Current,
    ExerciseDiaryRecord Target);

public record ExerciseDiaryRecord(
    int Steps,
    ICollection<CompletedExercise>? CompletedExercises,
    ExerciseDiaryRecordInfo Info);
    
public record CompletedExercise(
    Exercise Exercise,
    int? Repeats,
    int? TimeInSeconds);
    
public record ExerciseDiaryRecordInfo(
    float TotalKcalBurnt);