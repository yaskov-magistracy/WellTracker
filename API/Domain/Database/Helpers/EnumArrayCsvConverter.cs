using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Domain.Database.Helpers;

public class EnumArrayCsvConverter<T>(
    string delimiter = "|"
) : DefaultTypeConverter
    where T : struct, Enum
{
    // Запись в CSV (объект → строка)
    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is T[] array && array.Length > 0)
        {
            return string.Join(delimiter, array.Select(e => e.ToString()));
        }
        return string.Empty;
    }
    
    // Чтение из CSV (строка → объект)
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Array.Empty<T>();
        
        try
        {
            var values = text.Replace(" ", "").Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            var result = new T[values.Length];
            
            for (int i = 0; i < values.Length; i++)
            {
                if (Enum.TryParse<T>(values[i].Trim(), true, out var enumValue))
                {
                    result[i] = enumValue;
                }
                else
                {
                    // Альтернатива: можно парсить числовые значения
                    if (int.TryParse(values[i].Trim(), out var intValue) && 
                        Enum.IsDefined(typeof(T), intValue))
                    {
                        result[i] = (T)(object)intValue;
                    }
                    else
                    {
                        throw new FormatException($"Невозможно преобразовать '{values[i]}' в {typeof(T).Name}");
                    }
                }
            }
            
            return result;
        }
        catch (Exception ex)
        {
            // Можно логировать ошибку
            Console.WriteLine($"Ошибка преобразования '{text}' в массив {typeof(T).Name}: {ex.Message}");
            return Array.Empty<T>();
        }
    }
}