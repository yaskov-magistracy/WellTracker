using API.ApiInfrasturcture.DTO.Search;

namespace API.Modules.FoodsModule.DTO;

public class FoodSearchApiRequest : BaseApiSearchRequest
{
    public string SearchText { get; set; }
}