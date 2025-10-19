using Domain.FoodDiaries.DTO;

namespace Domain.FoodDiaries;

public interface IFoodDiariesRepository
{
    Task<FoodDiary?> GetByDate(Guid userId, DateOnly date);
    Task<bool> Exists(Guid userId, DateOnly date);
    Task<FoodDiary> CreateOrUpdate(Guid userId, DateOnly date, FoodDiaryUpdateEntity updateEntity);
}