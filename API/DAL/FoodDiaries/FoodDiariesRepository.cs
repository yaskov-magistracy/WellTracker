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
            entity.Breakfast = FoodDiariesMapper.ToEntity(updateEntity.Breakfast, foodsByIds);
        if (updateEntity.Lunch != null)
            entity.Lunch = FoodDiariesMapper.ToEntity(updateEntity.Lunch, foodsByIds);
        if (updateEntity.Snack != null)
            entity.Snack = FoodDiariesMapper.ToEntity(updateEntity.Snack, foodsByIds);
        if (updateEntity.Dinner != null)
            entity.Dinner = FoodDiariesMapper.ToEntity(updateEntity.Dinner, foodsByIds);

        UpdateTotalInfo(entity);
    }

    private void UpdateTotalInfo(FoodDiaryEntity entity)
    {
        entity.TotalEnergy = new();
        entity.TotalNutriments = new();
        
        Add(entity, entity.Breakfast);
        Add(entity, entity.Lunch);
        Add(entity, entity.Snack);
        Add(entity, entity.Dinner);
        
        void Add(FoodDiaryEntity entity, ICollection<MealEntity> meals)
        {
            foreach (var meal in meals)
            {
                var coef = ((float) meal.Grams / 100);
                entity.TotalEnergy.Kcal += meal.Food.Energy.Kcal * coef;
                entity.TotalEnergy.Kj += meal.Food.Energy.Kj * coef;
                entity.TotalNutriments.Protein += meal.Food.Nutriments.Protein * coef;
                entity.TotalNutriments.Fat += meal.Food.Nutriments.Fat * coef;
                entity.TotalNutriments.carbohydrates += meal.Food.Nutriments.carbohydrates * coef;
            }
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