using Domain.Foods.DTO;
using Infrastructure.Results;

namespace Domain.Foods;

public interface IFoodsService
{
    Task<Result<FoodSearchResponse>> Search(FoodSearchRequest request);
    Task<Result<Food>> GetById(Guid foodId);
    Task<EmptyResult> AddBatch(ICollection<FoodCreateEntity> request);
    Task<Result<Food>> Add(FoodCreateEntity request);
    Task<Result<Food>> Update(Guid foodId, FoodUpdateEntity request);
}

public class FoodsService(
    IFoodsRepository foodsRepository
) : IFoodsService
{
    public async Task<Result<FoodSearchResponse>> Search(FoodSearchRequest request)
    {
        var searchResponse = await foodsRepository.Search(request);
        return Results.Ok(searchResponse);
    }

    public async Task<Result<Food>> GetById(Guid foodId)
    {
        var food = await foodsRepository.Get(foodId);
        if (food == null)
            return Results.NotFound<Food>("");

        return Results.Ok(food);
    }

    public async Task<EmptyResult> AddBatch(ICollection<FoodCreateEntity> request)
    {
        await foodsRepository.AddBatch(request);
        return EmptyResults.NoContent();
    }

    public async Task<Result<Food>> Add(FoodCreateEntity request)
    {
        var created = await foodsRepository.Add(request);
        return Results.Ok(created);
    }

    public async Task<Result<Food>> Update(Guid foodId, FoodUpdateEntity request)
    {
        var exists = await foodsRepository.Exists(foodId);
        if (!exists)
            return Results.NotFound<Food>("");
        
        var updated = await foodsRepository.Update(foodId, request);
        return Results.Ok(updated);
    }
}