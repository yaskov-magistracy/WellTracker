using Domain.Database.Github;
using Domain.Foods;
using Domain.Foods.DTO;

namespace Domain.Database.FoodsFilling;

public interface IDatabaseFoodsFiller
{
    Task FillFromRepo(
        int? maxEntities = null,
        int maxInChunk = 20);
}

public class DatabaseFoodsFiller(
    IFoodsService foodsService,
    IGitHubFileReader gitHubFileReader
) : IDatabaseFoodsFiller
{
    public async Task FillFromRepo(
        int? maxEntities = null,
        int maxInChunk = 20)
    {
        var (csvReader, foods) = await gitHubFileReader.Read<FoodCreateEntity>("/FoodsParserV2/foods.csv");
        foreach (var foodBatch in foods
                 .Take(maxEntities ?? int.MaxValue)
                 .Chunk(maxInChunk))
        {
            await foodsService.AddBatch(foodBatch);
        }
        csvReader.Dispose();
    }
}