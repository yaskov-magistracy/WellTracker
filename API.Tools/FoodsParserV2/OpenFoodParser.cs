using System.Text.Json;
using FoodsParserV2.Models;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace FoodsParserV2;

public class OpenFoodFactsParser
{
    private readonly HttpClient httpClient;
    private const string BaseUrl = "https://world.openfoodfacts.org/api/v2/search";

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
                ["fields"] = "product_name,brands,manufacturer,nutriments,code,product_quantity"
            };

            var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
            var fullUrl = $"{BaseUrl}?{queryString}";

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
                    || string.IsNullOrWhiteSpace(product.product_name)
                    || product.nutriments == null
                    || product.nutriments.IsIncorrect())
                    continue;

                product.nutriments.Normalize();
                var productInfo = new Product
                {
                    Name = product.product_name,
                    GrammsInPortion = product.product_quantity,
                    Brand = product.brands,
                    Barcode = product.code,
                };

                var nutriments = product.nutriments;

                // Калории
                productInfo.Kcal = (float) GetNutrimentValue(nutriments.energy_kcal_100g, nutriments.energy_kcal);
                productInfo.Kj = (float) (GetNutrimentValue(nutriments.energy_100g, nutriments.energy)
                                          ?? productInfo.Kcal * 4.184);
                // Белки
                productInfo.Proteins = (float) GetNutrimentValue(nutriments.proteins_100g, nutriments.proteins);
                // Жиры
                productInfo.Fats = (float) GetNutrimentValue(nutriments.fat_100g, nutriments.fat);
                // Углеводы
                productInfo.Carbohydrates =
                    (float) GetNutrimentValue(nutriments.carbohydrates_100g, nutriments.carbohydrates);
                
                parsedProducts.Add(productInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при парсинге продукта: {ex.Message}");
            }
        }

        return parsedProducts;
    }

    private float? GetNutrimentValue(params float?[] values)
    {
        foreach (var value in values)
        {
            if (value.HasValue)
            {
                return MathF.Round(value.Value, 2);
            }
        }

        return null;
    }
}