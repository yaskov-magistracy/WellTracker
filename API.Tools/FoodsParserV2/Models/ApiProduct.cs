namespace FoodsParserV2.Models;

public class ApiProduct
{
    public string product_name { get; set; }
    public string brands { get; set; }
    public string code { get; set; }
    public Nutriments nutriments { get; set; }
}