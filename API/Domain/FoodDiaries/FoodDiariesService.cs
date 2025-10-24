using Domain.FoodDiaries.DTO;
using Infrastructure.Results;

namespace Domain.FoodDiaries;

public interface IFoodDiariesService
{
    Task<Result<FoodDiary>> GetByDate(Guid userId, DateOnly date);
    Task<Result<FoodDiary>> CreateOrUpdate(Guid userId, DateOnly date, FoodDiaryUpdateEntity updateEntity);
}

public class FoodDiariesService(
    IFoodDiariesRepository foodDiariesRepository
) : IFoodDiariesService
{

    public async Task<Result<FoodDiary>> GetByDate(Guid userId, DateOnly date)
    {
        var foodDiary = await foodDiariesRepository.GetByDate(userId, date);
        if (foodDiary == null)
            return Results.NotFound<FoodDiary>("");
        
        return Results.Ok(foodDiary);
    }

    public async Task<Result<FoodDiary>> CreateOrUpdate(Guid userId, DateOnly date, FoodDiaryUpdateEntity updateEntity)
    {
        var updated = await foodDiariesRepository.CreateOrUpdate(userId, date, updateEntity);
        return Results.Ok(updated);
    }
}