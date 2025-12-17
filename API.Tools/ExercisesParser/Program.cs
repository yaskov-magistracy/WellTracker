using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Domain.Database.Helpers;
using Domain.Exercises;
using Domain.Exercises.DTO;

var exercises = new List<ExerciseCreateEntity>()
{
    new("Жим штанги лёжа", "Ложитесь на скамью, локти 90 градусов от корпуса. Опускайте штангу под углом к груди", ExerciseType.Strength, ExerciseMeasurement.Repeats, [MuscleType.Chest, MuscleType.Triceps], [EquipmentType.Bench, EquipmentType.Barbell]),
};
await SaveToCsvAsync(exercises);

async Task SaveToCsvAsync(List<ExerciseCreateEntity> exercisesCreateEntities, string filename = "../../../exercises.csv")
{
    if (exercisesCreateEntities.Count == 0)
    {
        Console.WriteLine("Нет данных для сохранения");
        return;
    }

    try
    {
        using var writer = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            
        csv.Context.TypeConverterCache.AddConverter<MuscleType[]>(new EnumArrayCsvConverter<MuscleType>());
        csv.Context.TypeConverterCache.AddConverter<EquipmentType[]>(new EnumArrayCsvConverter<EquipmentType>());
        
        await csv.WriteRecordsAsync(exercisesCreateEntities);
        Console.WriteLine($"Данные сохранены в файл: {filename}");
        Console.WriteLine($"Всего сохранено продуктов: {exercisesCreateEntities.Count}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при сохранении в CSV: {ex.Message}");
    }
}