namespace FoodsParserV2.Models;

public class ApiResponse
{
    public List<ApiProduct> products { get; set; }
    public int count { get; set; }
    public int page { get; set; }
    public int page_size { get; set; }
}
