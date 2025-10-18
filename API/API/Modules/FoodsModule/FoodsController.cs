using API.Configuration.Auth;
using API.Modules.FoodsModule.DTO;
using Domain.Accounts;
using Domain.Foods;
using Domain.Foods.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.FoodsModule;

[Route("api/[controller]")]
[ApiController]
public class FoodsController(
    IFoodsService foodsService
) : ControllerBase
{
    /// <summary>
    /// Полнотекстовый поиск по продуктам
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [HttpPost("search")]
    public async Task<ActionResult<Food>> Search([FromBody] FoodSearchApiRequest request)
    {
        var response = await foodsService.Search(FoodsMapper.ToDomain(request));
        return response.ActionResult;
    }
    
    /// <summary>
    /// Получить продукт по Id
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [HttpGet("{foodId:Guid}")]
    public async Task<ActionResult<Food>> GetById([FromRoute] Guid foodId)
    {
        var response = await foodsService.GetById(foodId);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Добавление пачки продуктов
    /// </summary>
    /// <remarks>
    /// Авторизация по `Админу`
    /// </remarks>
    [AuthorizeRoles(AccountRole.Admin)]
    [HttpPost("batch")]
    public async Task<ActionResult<ICollection<Food>>> AddBatch([FromBody] ICollection<FoodCreateEntity> request)
    {
        var response = await foodsService.AddBatch(request);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Добавление 1 продукта
    /// </summary>
    /// <remarks>
    /// Авторизация по `Любой` роли
    /// </remarks>
    [Authorize]
    [HttpPost("")]
    public async Task<ActionResult<Food>> Add([FromBody] FoodCreateEntity request)
    {
        var response = await foodsService.Add(request);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Обновление продукта
    /// </summary>
    /// <remarks>
    /// Аториазция по `Любой` роли
    /// </remarks>
    [Authorize]
    [HttpPost("{foodId:Guid}")]
    public async Task<ActionResult<Food>> Update(
        [FromRoute] Guid foodId,
        [FromBody] FoodUpdateEntity updateRequest)
    {
        var response = await foodsService.Update(foodId, updateRequest);
        return response.ActionResult;
    }
}