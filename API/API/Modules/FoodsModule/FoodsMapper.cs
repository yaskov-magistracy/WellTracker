using API.Modules.FoodsModule.DTO;
using Domain.Foods.DTO;

namespace API.Modules.FoodsModule;

internal static class FoodsMapper
{
    public static FoodSearchRequest ToDomain(FoodSearchApiRequest request)
        => new(request.SearchText, request.ExcludedIds, request.Take, request.Skip);
}