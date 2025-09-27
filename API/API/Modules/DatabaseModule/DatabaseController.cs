using Domain.Database;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.DatabaseModule;

/// <summary>
/// Работа с БД
/// </summary>
[Route("api/database")]
[ApiController]
public class DatabaseController(
    IDatabaseService databaseService
) : ControllerBase
{
    /// <summary>
    /// Пересоздаёт БД
    /// </summary>
    /// <remarks>
    /// Если поставлен флаг withAutoFilling:
    /// Создаёт дефолтного Админа - admin/admin
    /// </remarks>
    [HttpPost("recreate")]
    public async Task<ActionResult> RecreateDatabase([FromQuery] bool withAutoFilling = true)
    {
        var response = await databaseService.RecreateDatabase(withAutoFilling);
        return response.ActionResult;
    }
}