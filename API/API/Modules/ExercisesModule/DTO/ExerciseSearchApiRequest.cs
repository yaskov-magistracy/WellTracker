using API.ApiInfrasturcture.DTO.Search;
using Domain.Exercises;

namespace API.Modules.ExercisesModule.DTO;

public class ExerciseSearchApiRequest : BaseApiSearchRequest
{
    public string? SearchText { get; set; }
    public ExerciseType? Type { get; set; }
    public MuscleType[]? Muscles { get; set; }
    public EquipmentType[]? Equipments { get; set; }
    public ExerciseMeasurement? Measurement { get; set; }
}