using Infrastructure.DTO.Search;

namespace Domain.Foods.DTO;

public record FoodSearchRequest(
    string SearchText,
    IReadOnlyCollection<Guid>? ExcludedIds,
    int Take = 100,
    int Skip = 0
) : BaseSearchRequest<Food>(Take, Skip)
{
}