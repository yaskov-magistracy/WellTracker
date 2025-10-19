using DAL.Foods;
using Domain.FoodDiaries;
using Domain.FoodDiaries.DTO;

namespace DAL.FoodDiaries;

internal static class FoodDiariesMapper
{
    public static ICollection<MealEntity> ToEntity(
        ICollection<MealUpdateEntity> updateEntity,
        Dictionary<Guid, FoodEntity> foodsByIds)
        => updateEntity.Select(e => ToEntity(e, foodsByIds)).ToArray();

    public static MealEntity ToEntity(
        MealUpdateEntity updateEntity,
        Dictionary<Guid, FoodEntity> foodsByIds)
        => new()
        {
            Food = foodsByIds[updateEntity.FoodId],
            Grams = updateEntity.Grams,
        };
    
    public static FoodDiary ToDomain(FoodDiaryEntity entity)
        => new(entity.Id, entity.UserId, entity.Date, 
            ToDomain(entity.Breakfast),
            ToDomain(entity.Lunch),
            ToDomain(entity.Snack),
            ToDomain(entity.Dinner),
            FoodsMapper.ToDomain(entity.TotalNutriments), 
            FoodsMapper.ToDomain(entity.TotalEnergy));

    public static ICollection<Meal> ToDomain(ICollection<MealEntity> entities)
        => entities.Select(ToDomain).ToArray();
    public static Meal ToDomain(MealEntity entity)
        => new(FoodsMapper.ToDomain(entity.Food), entity.Grams);
}