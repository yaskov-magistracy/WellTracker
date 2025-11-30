using API.Configuration.Auth;
using Domain.Accounts;
using Domain.ExerciseDiaries;
using Domain.ExerciseDiaries.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.ExerciseDiariesModule;

[Route("api/[controller]")]
[ApiController]
public class ExerciseDiaryController(
    IExerciseDiariesService exerciseDiariesService
) : ControllerBase
{
    /// <summary>
    /// Получить записи по дню
    /// </summary>
    /// <remarks>
    /// `date` - в формате `yyyy.MM.dd` или `yyyy-MM-dd`
    /// </remarks>>
    [AuthorizeRoles(AccountRole.User)]
    [HttpGet("{date:DateOnly}")]
    public async Task<ActionResult<ExerciseDiary>> GetByDate([FromRoute] DateOnly date)
    {
        var response = await exerciseDiariesService.GetByDate(User.GetId(), date);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Обновить записи по дню
    /// </summary>
    /// <remarks>
    /// `date` - в формате `yyyy.MM.dd` или `yyyy-MM-dd` <br/>
    /// `Полностью` переписывает данные, которые передаёшь. Если передашь `null` - ничего не будет
    /// </remarks>>
    [AuthorizeRoles(AccountRole.User)]
    [HttpPost("{date:DateOnly}")]
    public async Task<ActionResult<ExerciseDiary>> UpdateExerciseDiary([FromRoute] DateOnly date, ExerciseDiaryUpdateEntity updateEntity)
    {
        var response = await exerciseDiariesService.CreateOrUpdate(User.GetId(), date, updateEntity);
        return response.ActionResult;
    }
}