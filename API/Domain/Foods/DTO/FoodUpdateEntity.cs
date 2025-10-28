using Infrastructure.DTO;

namespace Domain.Foods.DTO;

public record FoodUpdateEntity(
    string? Name,
    NullablePatch<string>? BrandName,
    NullablePatch<float>? GramsInPortion,
    FoodNutrimentsUpdateEntity? Nutriments,
    FoodEnergyUpdateEntity? Energy)
{
}

public record FoodNutrimentsUpdateEntity(
    float? Protein,
    float? Fat,
    float? carbohydrates)
{
}

public record FoodEnergyUpdateEntity(
    float? Kcal,
    float? Kj)
{
}