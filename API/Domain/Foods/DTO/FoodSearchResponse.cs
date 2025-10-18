using Infrastructure.DTO.Search;

namespace Domain.Foods.DTO;

public record FoodSearchResponse(
    ICollection<Food> Items,
    int TotalCount
) : BaseSearchResponse<Food>(Items, TotalCount);