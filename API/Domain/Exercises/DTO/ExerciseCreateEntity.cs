namespace Domain.Exercises.DTO;

public record ExerciseCreateEntity(
    string Name,
    string Description,
    ExerciseType Type,
    ExerciseMeasurement Measurement,
    MuscleType[] Muscles,
    EquipmentType[] Equipments)
{
    
}