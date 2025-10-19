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
    /// <summary>
    /// Получить записи по дню
    /// </summary>
    /// <remarks>
    /// `date` - в формате `dd.MM.yyyy` или `dd-MM-yyyy`
    /// </remarks>>
    [AuthorizeRoles(AccountRole.User)]
    [HttpGet("{date:DateOnly}")]
    public async Task<ActionResult<FoodDiary>> GetByDate([FromRoute] DateOnly date)
    {
        var response = await foodDiariesService.GetByDate(User.GetId(), date);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Обновить записи по дню
    /// </summary>
    /// <remarks>
    /// `date` - в формате `dd.MM.yyyy` или `dd-MM-yyyy` <br/>
    /// `Полностью` переписывает приём пищи, который ему передаешь
    /// </remarks>>
    [AuthorizeRoles(AccountRole.User)]
    [HttpPost("{date:DateOnly}")]
    public async Task<ActionResult<FoodDiary>> UpdateFoodDiary([FromRoute] DateOnly date, FoodDiaryUpdateEntity updateEntity)
    {
        var response = await foodDiariesService.CreateOrUpdate(User.GetId(), date, updateEntity);
        return response.ActionResult;
    }
}