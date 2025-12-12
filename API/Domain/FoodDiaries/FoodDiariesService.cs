using Domain.Advices;
using Domain.FoodDiaries.DTO;
using Domain.Foods;
using Infrastructure.Results;

namespace Domain.FoodDiaries;

public interface IFoodDiariesService
{
    Task<Result<FoodDiary>> GetByDate(Guid userId, DateOnly date);
    Task<Result<FoodDiary>> CreateOrUpdate(Guid userId, DateOnly date, FoodDiaryUpdateEntity updateEntity);
}

public class FoodDiariesService(
    IFoodDiariesRepository foodDiariesRepository,
    IAdvicesService advicesService
) : IFoodDiariesService
{

    public async Task<Result<FoodDiary>> GetByDate(Guid userId, DateOnly date)
    {
        var foodDiary = await foodDiariesRepository.GetByDate(userId, date);
        foodDiary ??= new FoodDiary(Guid.Empty, userId, date, null,null,null,null,FoodNutriments.Empty(), FoodEnergy.Empty(),FoodNutriments.Empty(), FoodEnergy.Empty());

        var targets = await advicesService.GetTargets(userId);
        foodDiary = foodDiary with {TargetNutriments = targets.Nutriments, TargetEnergy = targets.Energy};
        
        return Results.Ok(foodDiary);
    }

    public async Task<Result<FoodDiary>> CreateOrUpdate(Guid userId, DateOnly date, FoodDiaryUpdateEntity updateEntity)
    {
        var updated = await foodDiariesRepository.CreateOrUpdate(userId, date, updateEntity);
        return Results.Ok(updated);
    }
}