using DAL.Foods;
using Domain.FoodDiaries;

namespace DAL.FoodDiaries;

internal static class FoodDiariesMapper
{
    public static FoodDiary ToDomain(FoodDiaryEntity entity)
        => new(entity.Id, entity.UserId, entity.Date, 
            entity.Breakfast == null ? null : ToDomain(entity.Breakfast),
            entity.Lunch == null ? null : ToDomain(entity.Lunch),
            entity.Snack == null ? null : ToDomain(entity.Snack),
            entity.Dinner == null ? null : ToDomain(entity.Dinner),
            FoodsMapper.ToDomain(entity.TotalNutriments), 
            FoodsMapper.ToDomain(entity.TotalEnergy),
            null,
            null);

    public static Meal ToDomain(MealEntity entity)
        => new(
            entity.EatenFoods.Select(ToDomain).ToArray(), 
            FoodsMapper.ToDomain(entity.TotalNutriments), 
            FoodsMapper.ToDomain(entity.TotalEnergy));

    public static EatenFood ToDomain(EatenFoodEntity entity)
        => new(
            FoodsMapper.ToDomain(entity.Food),
            entity.Grams);
}