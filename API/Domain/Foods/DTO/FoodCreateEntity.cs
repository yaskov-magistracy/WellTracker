namespace Domain.Foods.DTO;

public record FoodCreateEntity(
    string Name,
    string? BrandName,
    float? GramsInPortion,
    FoodNutrimentsCreateEntity Nutriments,
    FoodEnergyCreateEntity Energy)
{
}

public record FoodNutrimentsCreateEntity(
    float Protein,
    float Fat,
    float carbohydrates)
{
}

public record FoodEnergyCreateEntity(
    float Kcal,
    float Kj)
{
}