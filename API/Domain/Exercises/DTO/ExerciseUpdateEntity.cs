namespace Domain.Exercises.DTO;

public record ExerciseUpdateEntity(
    string? Name,
    string? Description,
    ExerciseType? Type,
    ExerciseMeasurement? Measurement,
    MuscleType[]? Muscles,
    EquipmentType[]? Equipments)
{
    
}