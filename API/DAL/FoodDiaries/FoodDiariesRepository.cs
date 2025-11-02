using DAL.Foods;
using Domain.FoodDiaries;
using Domain.FoodDiaries.DTO;
using Domain.Foods;
using Microsoft.EntityFrameworkCore;

namespace DAL.FoodDiaries;

public class FoodDiariesRepository(
    DataContext dataContext
) : IFoodDiariesRepository
{
    private DbSet<FoodDiaryEntity> FoodDiaries => dataContext.FoodDiaries;
    
    public async Task<FoodDiary?> GetByDate(Guid userId, DateOnly date)
    {
        var entity = await GetByDateInternal(userId, date);
        return entity != null
            ? FoodDiariesMapper.ToDomain(entity)
            : null;
    }
    
    public async Task<bool> Exists(Guid userId, DateOnly date)
    {
        var entity = await GetByDateInternal(userId, date);
        return entity != null;
    }

    public async Task<FoodDiary> CreateOrUpdate(Guid userId, DateOnly date, FoodDiaryUpdateEntity updateEntity)
    {
        var entity = await GetByDateInternal(userId, date, asNoTracking: false);
        if (entity == null)
        {
            entity = new()
            {
                Date = date,
                UserId = userId,
            };
            await UpdateEntity(entity, updateEntity);
            await FoodDiaries.AddAsync(entity);
        }
        else
        {
            await UpdateEntity(entity, updateEntity);
        }
        
        await dataContext.SaveChangesAsync();
        return FoodDiariesMapper.ToDomain(entity);
    }

    private async Task UpdateEntity(FoodDiaryEntity entity, FoodDiaryUpdateEntity updateEntity)
    {
        var foodsByIds = await GetFoods(updateEntity);
        if (updateEntity.Breakfast != null)
            entity.Breakfast = BuildMeal(updateEntity.Breakfast, foodsByIds);
        if (updateEntity.Lunch != null)
            entity.Lunch = BuildMeal(updateEntity.Lunch, foodsByIds);
        if (updateEntity.Snack != null)
            entity.Snack = BuildMeal(updateEntity.Snack, foodsByIds);
        if (updateEntity.Dinner != null)
            entity.Dinner = BuildMeal(updateEntity.Dinner, foodsByIds);

        CountTotalInfo(entity);
    }

    private MealEntity BuildMeal(ICollection<EatenFoodUpdateEntity> newEatenFoods, Dictionary<Guid, FoodEntity> foodsByIds)
    {
        var eatenFoods = newEatenFoods.Select(e => new EatenFoodEntity()
        {
            Food = foodsByIds[e.FoodId],
            Grams = e.Grams,
        }).ToArray();
        
        return new()
        {
            EatenFoods = eatenFoods,
            TotalNutriments = new()
            {
                Protein = eatenFoods.Sum(e => e.Food.Nutriments.Protein * (e.Grams / 100f)),
                Fat = eatenFoods.Sum(e => e.Food.Nutriments.Fat * (e.Grams / 100f)),
                Carbohydrates = eatenFoods.Sum(e => e.Food.Nutriments.Carbohydrates * (e.Grams / 100f)),
            },
            TotalEnergy = new()
            {
                Kcal = eatenFoods.Sum(e => e.Food.Energy.Kcal * (e.Grams / 100f)),
                Kj = eatenFoods.Sum(e => e.Food.Energy.Kj * (e.Grams / 100f)),
            }
        };
    }

    private void CountTotalInfo(FoodDiaryEntity entity)
    {
        entity.TotalEnergy = new();
        entity.TotalNutriments = new();
        
        Add(entity.Breakfast);
        Add(entity.Lunch);
        Add(entity.Snack);
        Add(entity.Dinner);
        
        void Add(MealEntity? meal)
        {
            if (meal == null)
                return;

            entity.TotalNutriments.Protein += meal.TotalNutriments.Protein;
            entity.TotalNutriments.Fat += meal.TotalNutriments.Fat;
            entity.TotalNutriments.Carbohydrates += meal.TotalNutriments.Carbohydrates;
            
            entity.TotalEnergy.Kcal += meal.TotalEnergy.Kcal;
            entity.TotalEnergy.Kj += meal.TotalEnergy.Kj;
        }
    }

    private async Task<Dictionary<Guid, FoodEntity>> GetFoods(FoodDiaryUpdateEntity updateEntity)
    {
        var result = new Dictionary<Guid, FoodEntity>();
        var foodsIds =
            (updateEntity.Breakfast?.Select(e => e.FoodId) ?? [])
            .Concat(updateEntity.Lunch?.Select(e => e.FoodId) ?? [])
            .Concat(updateEntity.Snack?.Select(e => e.FoodId) ?? [])
            .Concat(updateEntity.Dinner?.Select(e => e.FoodId) ?? [])
            .Distinct();
        foreach (var foodId in foodsIds)
        {
            // TODO fix use dataContext
            var food = await dataContext.Foods.AsNoTracking().FirstOrDefaultAsync(e => e.Id == foodId);
            if (food == null)
                throw new Exception($"There is no food with id {foodId}");
            result.Add(foodId, food);
        }
        return result;
    }

    private Task<FoodDiaryEntity?> GetByDateInternal(Guid userId, DateOnly date, bool asNoTracking = true)
    {
        var query = FoodDiaries.AsQueryable();
        if (asNoTracking)
            query = query.AsNoTracking();
        
        return query
            .Where(e => e.UserId == userId && e.Date == date)
            .FirstOrDefaultAsync();
    }
}