using ExercisesParser.Models;
using Newtonsoft.Json;

namespace ExercisesParser;

class WgerParser
{
    private readonly HttpClient _httpClient;

    public WgerParser()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ExerciseApp/1.0");
    }

    public async Task DisplayExercisesWithDetails(int limit = 10)
    {
        try
        {
            var exercises = await GetExercisesWithDetails(limit);
            
            Console.WriteLine($"🚀 Найдено {exercises.Count} упражнений:\n");
            
            foreach (var exercise in exercises)
            {
                Console.WriteLine($"🏋️ {exercise.Name}");
                Console.WriteLine($"   📝 {GetCleanDescription(exercise.Description)}");
                Console.WriteLine($"   📁 Категория: {exercise.CategoryName}");
                
                if (exercise.MuscleNames.Any())
                    Console.WriteLine($"   💪 Основные мышцы: {string.Join(", ", exercise.MuscleNames)}");
                
                if (exercise.SecondaryMuscleNames.Any())
                    Console.WriteLine($"   🔧 Второстепенные: {string.Join(", ", exercise.SecondaryMuscleNames)}");
                
                if (exercise.EquipmentNames.Any())
                    Console.WriteLine($"   🛠️ Оборудование: {string.Join(", ", exercise.EquipmentNames)}");
                
                Console.WriteLine();
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"❌ Ошибка сети: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка: {ex.Message}");
        }
    }

    private async Task<List<DetailedExercise>> GetExercisesWithDetails(int limit)
    {
        var exercisesUrl = $"https://wger.de/api/v2/exercise/?limit={limit}&language=2";
        var exercisesResponse = await _httpClient.GetAsync(exercisesUrl);
        exercisesResponse.EnsureSuccessStatusCode();
        
        var exercisesJson = await exercisesResponse.Content.ReadAsStringAsync();
        var exercisesData = JsonConvert.DeserializeObject<WgerApiResponse<Exercise>>(exercisesJson);

        // Получаем дополнительные справочники
        var muscles = await GetMuscles();
        var equipment = await GetEquipment();
        var categories = await GetCategories();

        var detailedExercises = new List<DetailedExercise>();

        foreach (var exercise in exercisesData.Results)
        {
            var detailedExercise = new DetailedExercise
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                CategoryId = exercise.Category,
                CategoryName = categories.GetValueOrDefault(exercise.Category, "Неизвестно"),
                MuscleNames = exercise.Muscles.Select(m => muscles.GetValueOrDefault(m, "Неизвестно")).ToList(),
                SecondaryMuscleNames = exercise.MusclesSecondary.Select(m => muscles.GetValueOrDefault(m, "Неизвестно")).ToList(),
                EquipmentNames = exercise.Equipment.Select(e => equipment.GetValueOrDefault(e, "Неизвестно")).ToList()
            };
            
            detailedExercises.Add(detailedExercise);
        }

        return detailedExercises;
    }

    private async Task<Dictionary<int, string>> GetMuscles()
    {
        var url = "https://wger.de/api/v2/muscle/?limit=100";
        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<WgerApiResponse<Muscle>>(json);
        
        return data.Results.ToDictionary(m => m.Id, m => m.Name);
    }

    private async Task<Dictionary<int, string>> GetEquipment()
    {
        var url = "https://wger.de/api/v2/equipment/?limit=50";
        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<WgerApiResponse<Equipment>>(json);
        
        return data.Results.ToDictionary(e => e.Id, e => e.Name);
    }

    private async Task<Dictionary<int, string>> GetCategories()
    {
        var url = "https://wger.de/api/v2/exercisecategory/?limit=20";
        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<WgerApiResponse<Category>>(json);
        
        return data.Results.ToDictionary(c => c.Id, c => c.Name);
    }

    private string GetCleanDescription(string description, int maxLength = 120)
    {
        if (string.IsNullOrWhiteSpace(description))
            return "Описание отсутствует";
        
        // Убираем HTML теги
        var clean = System.Text.RegularExpressions.Regex.Replace(description, "<.*?>", string.Empty);
        clean = System.Text.RegularExpressions.Regex.Replace(clean, "&.*?;", " ");
        clean = clean.Trim();
        
        return clean.Length <= maxLength ? clean : clean.Substring(0, maxLength) + "...";
    }
}
