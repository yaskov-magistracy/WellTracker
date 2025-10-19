using API.Configuration.Auth;
using Domain.Accounts;
using Domain.FoodDiaries;
using Domain.FoodDiaries.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.FoodsDiariesModule;

[Route("api/[controller]")]
[ApiController]
public class FoodsDiaryController(
    IFoodDiariesService foodDiariesService
) : ControllerBase
{
    [AuthorizeRoles(AccountRole.User)]
    [HttpGet("{date:DateOnly}")]
    public async Task<ActionResult<FoodDiary>> GetByDate([FromRoute] DateOnly date)
    {
        var response = await foodDiariesService.GetByDate(User.GetId(), date);
        return response.ActionResult;
    }
    
    [AuthorizeRoles(AccountRole.User)]
    [HttpPost("{date:DateOnly}")]
    public async Task<ActionResult<FoodDiary>> UpdateFoodDiary([FromRoute] DateOnly date, FoodDiaryUpdateEntity updateEntity)
    {
        var response = await foodDiariesService.CreateOrUpdate(User.GetId(), date, updateEntity);
        return response.ActionResult;
    }
}