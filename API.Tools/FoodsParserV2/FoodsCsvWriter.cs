using System.Globalization;
using CsvHelper;
using Domain.Foods.DTO;
using FoodsParserV2.Models;

namespace FoodsParserV2;

internal class FoodsCsvWriter
{
    
    public static async Task SaveToCsvAsync(List<Product> products, string filename = "../../../foods.csv")
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Нет данных для сохранения");
            return;
        }

        try
        {
            var foodCreateEntities = products.Select(ToFoodEntity).ToList();
            using var writer = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            
            await csv.WriteRecordsAsync(foodCreateEntities);
            Console.WriteLine($"Данные сохранены в файл: {filename}");
            Console.WriteLine($"Всего сохранено продуктов: {foodCreateEntities.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении в CSV: {ex.Message}");
        }
    }

    private static FoodCreateEntity ToFoodEntity(Product product)
        => new(product.Name,
            product.Brand,
            product.GrammsInPortion,
            ToNutriments(product),
            ToEnergy(product)
        );

    private static FoodNutrimentsCreateEntity ToNutriments(Product product)
        => new(product.Proteins, product.Fats, product.Carbohydrates);
    
    private static FoodEnergyCreateEntity ToEnergy(Product product)
        => new(product.Kcal, product.Kj);
}