namespace FoodsParserV2.Models;

public class Product
{
    public string Name { get; set; } = "Неизвестно";
    public string Brand { get; set; } = "Неизвестно";
    public string Manufacturer { get; set; } = "Неизвестно";
    public string Barcode { get; set; } = "";
    public double? Calories { get; set; }
    public double? Proteins { get; set; }
    public double? Fats { get; set; }
    public double? Carbohydrates { get; set; }
}