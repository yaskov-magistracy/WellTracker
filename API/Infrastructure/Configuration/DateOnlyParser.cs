using System.Globalization;

namespace Infrastructure.Configuration;

public static class DateOnlyParser
{
    public static readonly string[] ParseFormats = ["dd.MM.yyyy", "dd-MM-yyyy"];

    public static bool TryParse(string strDate, out DateOnly date)
        => DateOnly.TryParseExact(strDate, ParseFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
}