using Domain.Database;
using Domain.Database.DTO;
using Domain.Database.FoodsFilling;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.DatabaseModule;

/// <summary>
/// Работа с БД
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DatabaseController(
    IDatabaseService databaseService
) : ControllerBase
{
    /// <summary>
    /// Пересоздаёт БД, чистит статические файлы
    /// </summary>
    /// <remarks>
    /// Параметры реквеста: <br/>
    /// <strong>AutoFillingParams</strong>: <br/>
    /// `OnlySimpleData` - Только Создаёт дефолтного Админа - admin/admin. Создаёт дефолтного Юзера - user/user. <br/>
    /// `SomeRealData` - Дополнительно Заполняет 100 продуктов/упражнений из csv в репозитории <br/>
    /// `FullRealData` - Дополнительно Полностью заполняет БД данными из csv в репозитории <br/>
    /// </remarks>
    [HttpPost("recreate")]
    public async Task<ActionResult> RecreateDatabase([FromBody] RecreateDatabaseRequest request)
    {
        var response = await databaseService.RecreateDatabase(request);
        return response.ActionResult;
    }
    
    /// <summary>
    /// Добавляет все продукты которые есть в csv в репозитории
    /// </summary>
    [HttpPost("foods")]
    public async Task<ActionResult> AddFullFoods()
    {
        // TODO: Добавить чистку перед добавлением?
        await databaseService.FillFromRepo();
        return NoContent();
    }
}