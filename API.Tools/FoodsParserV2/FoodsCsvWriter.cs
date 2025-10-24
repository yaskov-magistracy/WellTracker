using System.Globalization;
using CsvHelper;
using FoodsParserV2.Models;

namespace FoodsParserV2;

internal class FoodsCsvWriter
{
    
    public static async Task SaveToCsvAsync(List<Product> products, string filename = "../../../foods.csv")
    {
        if (products == null || products.Count == 0)
        {
            Console.WriteLine("Нет данных для сохранения");
            return;
        }

        try
        {
            using var writer = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            
            await csv.WriteRecordsAsync(products);
            Console.WriteLine($"Данные сохранены в файл: {filename}");
            Console.WriteLine($"Всего сохранено продуктов: {products.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении в CSV: {ex.Message}");
        }
    }
}