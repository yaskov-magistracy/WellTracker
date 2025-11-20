namespace FoodsParserV2.Models;

public class Product
{
    public string Name { get; set; } = "Неизвестно";
    public float? GrammsInPortion { get; set; }
    public string? Brand { get; set; }
    public string? Manufacturer { get; set; }
    public string? Barcode { get; set; }
    public float Kj { get; set; }
    public float Kcal { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
}