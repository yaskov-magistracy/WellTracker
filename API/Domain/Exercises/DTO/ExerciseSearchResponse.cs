using Infrastructure.DTO.Search;

namespace Domain.Exercises.DTO;

public record ExerciseSearchResponse(
    ICollection<Exercise> Items,
    int TotalCount
) : BaseSearchResponse<Exercise>(Items, TotalCount);