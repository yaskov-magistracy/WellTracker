using Domain.Database;
using Domain.Database.FoodsFilling;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.DatabaseModule;

/// <summary>
/// Работа с БД
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DatabaseController(
    IDatabaseService databaseService,
    IDatabaseFoodsFiller databaseFoodsFiller
) : ControllerBase
{
    /// <summary>
    /// Пересоздаёт БД, чистит статические файлы
    /// </summary>
    /// <remarks>
    /// Если поставлен флаг withAutoFilling: <br/> 
    /// Создаёт дефолтного Админа - admin/admin. <br/>
    /// Создаёт дефолтного Юзера - user/user. <br/>
    /// Заполняет 100 продуктов из csv в репозитории
    /// </remarks>
    [HttpPost("recreate")]
    public async Task<ActionResult> RecreateDatabase([FromQuery] bool withAutoFilling = true)
    {
        var response = await databaseService.RecreateDatabase(withAutoFilling);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Добавляет все продукты которые есть в csv в репозитории
    /// </summary>
    [HttpPost("foods")]
    public async Task<ActionResult> AddFullFoods()
    {
        // TODO: Добавить чистку перед добавлением?
        await databaseFoodsFiller.FillFromRepo();
        return NoContent();
    }
}