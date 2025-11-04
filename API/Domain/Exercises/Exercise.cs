namespace Domain.Exercises;

public record Exercise(
    Guid Id,
    string Name,
    string Description,
    ExerciseType Type,
    ExerciseMeasurement Measurement,
    MuscleType[] Muscles,
    EquipmentType[] Equipments
    )
{
}