using System.Text.Json.Serialization;
using JsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;

namespace Infrastructure.Configuration.Serialization;

public class JsonConverters
{
    public static void ConfigureJson(JsonOptions options)
    {
        var enumConverter = new JsonStringEnumConverter();
        options.JsonSerializerOptions.Converters.Add(enumConverter);
    }
}