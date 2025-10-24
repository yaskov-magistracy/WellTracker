namespace Domain.Foods;

public record Food(
    Guid Id,
    string Name,
    string? BrandName,
    float? GramsInPortion,
    FoodNutriments Nutriments,
    FoodEnergy Energy)
{
}
