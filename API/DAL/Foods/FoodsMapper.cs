using Domain.Foods;
using Domain.Foods.DTO;

namespace DAL.Foods;

internal static class FoodsMapper
{
    public static FoodEntity ToEntity(FoodCreateEntity createEntity)
        => new()
        {
            Name = createEntity.Name,
            BrandName = createEntity.BrandName,
            GramsInPortion = createEntity.GramsInPortion,
            Nutriments = ToEntity(createEntity.Nutriments),
            Energy = ToEntity(createEntity.Energy),
        };

    public static FoodNutrimentsEntity ToEntity(FoodNutrimentsCreateEntity createEntity)
        => new()
        {
            Protein = createEntity.Protein,
            Fat = createEntity.Fat,
            Сarbohydrates = createEntity.Сarbohydrates,
        };

    private static FoodEnergyEntity ToEntity(FoodEnergyCreateEntity createEntity)
        => new()
        {
            Kcal = createEntity.Kcal,
            Kj = createEntity.Kj,
        };

    public static Food ToDomain(FoodEntity entity)
        => new(entity.Id, entity.Name, entity.BrandName, entity.GramsInPortion, ToDomain(entity.Nutriments), ToDomain(entity.Energy));

    public static FoodNutriments ToDomain(FoodNutrimentsEntity entity)
        => new(entity.Protein, entity.Fat, entity.Сarbohydrates);

    private static FoodEnergy ToDomain(FoodEnergyEntity entity)
        => new(entity.Kcal, entity.Kj);
}