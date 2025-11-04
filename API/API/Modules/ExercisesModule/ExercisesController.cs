using API.Configuration.Auth;
using API.Modules.ExercisesModule.DTO;
using Domain.Accounts;
using Domain.Exercises;
using Domain.Exercises.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.ExercisesModule;

[Route("api/[controller]")]
[ApiController]
public class ExercisesController(
    IExercisesService exercisesService
) : ControllerBase
{
    /// <summary>
    /// Полнотекстовый поиск по упражнениям
    /// </summary>
    /// <remarks>
    /// `SearchText` - поиск по Name + Description
    /// </remarks>
    [HttpPost("search")]
    public async Task<ActionResult<Exercise>> Search([FromBody] ExerciseSearchApiRequest request)
    {
        var response = await exercisesService.Search(ExercisesMapper.ToDomain(request));
        return response.ActionResult;
    }
    
    /// <summary>
    /// Получить упражнение по Id
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [HttpGet("{exerciseId:Guid}")]
    public async Task<ActionResult<Exercise>> GetById([FromRoute] Guid exerciseId)
    {
        var response = await exercisesService.GetById(exerciseId);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Добавление пачки упражнений
    /// </summary>
    /// <remarks>
    /// Авторизация по `Админу`
    /// </remarks>
    [AuthorizeRoles(AccountRole.Admin)]
    [HttpPost("batch")]
    public async Task<ActionResult<ICollection<Exercise>>> AddBatch([FromBody] ICollection<ExerciseCreateEntity> request)
    {
        var response = await exercisesService.AddBatch(request);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Добавление 1 упражнения
    /// </summary>
    /// <remarks>
    /// Авторизация по `Любой` роли
    /// </remarks>
    [Authorize]
    [HttpPost("")]
    public async Task<ActionResult<Exercise>> Add([FromBody] ExerciseCreateEntity request)
    {
        var response = await exercisesService.Add(request);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Обновление упражнения
    /// </summary>
    /// <remarks>
    /// Аториазция по `Любой` роли
    /// </remarks>
    [Authorize]
    [HttpPost("{exerciseId:Guid}")]
    public async Task<ActionResult<Exercise>> Update(
        [FromRoute] Guid exerciseId,
        [FromBody] ExerciseUpdateEntity updateRequest)
    {
        var response = await exercisesService.Update(exerciseId, updateRequest);
        return response.ActionResult;
    }
}