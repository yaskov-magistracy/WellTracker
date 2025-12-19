using System.Globalization;
using CsvHelper;
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
        
        async Task Test()
        {
            using var a =
                File.OpenRead("C:\\Users\\yasko\\Desktop\\Мага\\WellTracker\\API.Tools\\FoodsParserV2\\foods.csv");
            using var reader = new StreamReader(a);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<FoodCreateEntity>().ToList();
        }
    }
}