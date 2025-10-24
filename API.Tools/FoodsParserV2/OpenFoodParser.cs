using System.Text.Json;
using FoodsParserV2.Models;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace FoodsParserV2;


public class OpenFoodFactsParser
{
    private readonly HttpClient httpClient;
    private const string BaseUrl = "https://world.openfoodfacts.org";

    public OpenFoodFactsParser()
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "WellTrackerParser/1.0");
        httpClient.Timeout = TimeSpan.FromSeconds(60);
    }

    public async Task<List<Product>> GetRussianProductsAsync(int pageSize = 100, int maxPages = 10)
    {
        var products = new List<Product>();

        for (int page = 1; page <= maxPages; page++)
        {
            Console.WriteLine($"Парсинг страницы {page}...");

            var parameters = new Dictionary<string, string>
            {
                ["countries_tags"] = "Russia",
                ["page_size"] = pageSize.ToString(),
                ["page"] = page.ToString(),
                ["fields"] = "product_name,brands,manufacturer,nutriments,code"
            };

            var url = $"{BaseUrl}/api/v2/search";
            var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
            var fullUrl = $"{url}?{queryString}";

            try
            {
                var response = await httpClient.GetAsync(fullUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

                if (apiResponse?.products == null || apiResponse.products.Count == 0)
                {
                    Console.WriteLine("Больше продуктов нет");
                    break;
                }

                var parsedProducts = ParseProducts(apiResponse.products);
                products.AddRange(parsedProducts);

                Console.WriteLine($"Получено {parsedProducts.Count} продуктов со страницы {page}");

                // Задержка чтобы не перегружать сервер
                await Task.Delay(1000);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при запросе страницы {page}: {ex.Message}");
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка на странице {page}: {ex.Message}");
                break;
            }
        }

        return products;
    }

    private List<Product> ParseProducts(List<ApiProduct> rawProducts)
    {
        var parsedProducts = new List<Product>();

        foreach (var product in rawProducts)
        {
            try
            {
                if (string.IsNullOrEmpty(product.product_name)
                    || string.IsNullOrWhiteSpace(product.product_name))
                    continue;
                
                var productInfo = new Product
                {
                    Name = product.product_name,
                    Brand = product.brands ?? "",
                    Barcode = product.code ?? ""
                };

                var nutriments = product.nutriments;

                if (nutriments != null)
                {
                    // Калории
                    productInfo.Calories = GetNutrimentValue(
                        nutriments.energy_kcal_100g,
                        nutriments.energy_100g,
                        nutriments.energy_kcal,
                        nutriments.energy);

                    // Белки
                    productInfo.Proteins = GetNutrimentValue(
                        nutriments.proteins_100g,
                        nutriments.proteins);

                    // Жиры
                    productInfo.Fats = GetNutrimentValue(
                        nutriments.fat_100g,
                        nutriments.fat);

                    // Углеводы
                    productInfo.Carbohydrates = GetNutrimentValue(
                        nutriments.carbohydrates_100g,
                        nutriments.carbohydrates);
                }

                // Фильтруем продукты без минимальной информации
                if (productInfo.Name != "Неизвестно" &&
                    (productInfo.Calories.HasValue || productInfo.Proteins.HasValue ||
                     productInfo.Fats.HasValue || productInfo.Carbohydrates.HasValue))
                {
                    parsedProducts.Add(productInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при парсинге продукта: {ex.Message}");
                continue;
            }
        }

        return parsedProducts;
    }

    private double? GetNutrimentValue(params double?[] values)
    {
        foreach (var value in values)
        {
            if (value.HasValue)
            {
                return Math.Round(value.Value, 2);
            }
        }
        return null;
    }

    public void DisplaySample(List<Product> products, int numSamples = 5)
    {
        Console.WriteLine($"\nПримеры найденных продуктов (первые {numSamples}):");
        Console.WriteLine(new string('-', 80));

        for (int i = 0; i < Math.Min(numSamples, products.Count); i++)
        {
            var product = products[i];
            Console.WriteLine($"{i + 1}. {product.Name}");
            Console.WriteLine($"   Бренд: {product.Brand}");
            Console.WriteLine($"   Производитель: {product.Manufacturer}");
            Console.WriteLine($"   КБЖУ на 100г: " +
                              $"Кал: {product.Calories?.ToString() ?? "N/A"}, " +
                              $"Б: {product.Proteins?.ToString() ?? "N/A"}г, " +
                              $"Ж: {product.Fats?.ToString() ?? "N/A"}г, " +
                              $"У: {product.Carbohydrates?.ToString() ?? "N/A"}г");
            Console.WriteLine();
        }
    }
}