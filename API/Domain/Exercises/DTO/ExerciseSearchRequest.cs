using Infrastructure.DTO.Search;

namespace Domain.Exercises.DTO;

public record ExerciseSearchRequest(
    string? SearchText,
    ExerciseType? Type,
    MuscleType[]? Muscles,
    EquipmentType[]? Equipments,
    ExerciseMeasurement? Measurement,
    int Take = 100,
    int Skip = 0
) : BaseSearchRequest<Exercise>(Take, Skip)
{
    
}