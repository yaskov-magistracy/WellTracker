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
    [HttpPost("recreate")]
    public async Task<ActionResult> RecreateDatabase()
    {
        var response = await databaseService.RecreateDatabase();
        return response.ActionResult;
    }
}