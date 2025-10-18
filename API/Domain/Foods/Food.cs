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

public record FoodNutriments(
    float Protein,
    float Fat,
    float Сarbohydrates)
{
}

public record FoodEnergy(
    float Kcal,
    float Kj)
{
}