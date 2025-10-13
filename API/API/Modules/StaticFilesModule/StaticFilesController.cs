using API.Configuration.Auth;
using API.Modules.StaticFilesModule.DTO;
using Domain.Accounts;
using Domain.StaticFiles;
using Infrastructure.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.StaticFilesModule;

[Route("api/statics")]
[ApiController]
public class StaticFilesController(
    IStaticFilesService staticFilesService
) : ControllerBase
{
    private static readonly string[] AllowedTypes = {"image/jpg", "image/png", "image/jpeg"};

    /// <summary>
    /// Загружает файл на бэк 
    /// </summary>
    /// <remarks>
    /// Поддерживаемые форматы: jpg, png, jpeg
    /// </remarks>>
    /// TODO: Добавить ограничение на размер файлов
    [AuthorizeRoles(AccountRole.Admin)]
    [Consumes("multipart/form-data")]
    [HttpPost("upload")]
    public async Task<ActionResult<UploadFileResponse>> Upload([FromForm] UploadFileRequest request)
    {
        if (!AllowedTypes.Contains(request.File.ContentType) )
            return BadRequest($"Неподдерживаемый тип файла: {request.File.ContentType}. Подерижваются только типы: {string.Join(";", AllowedTypes)}");

        var response = await staticFilesService.Create(StaticFilesMapper.ToDomain(request));
        return response
            .Map(StaticFilesMapper.ToResponse)
            .ActionResult;
    }

    /// <summary>
    /// Получение файла
    /// </summary>
    /// <remarks>
    /// Для встройки в фронт достаточно просто прописать аттрибут src=".../api/statics/{id}"
    /// </remarks>
    [HttpGet("{imageId:Guid}")]
    public async Task<ActionResult> Download([FromRoute] Guid imageId)
    {
        var result = await staticFilesService.Get(imageId);
        if (!result.IsSuccess)
            return result.ActionResult;

        var staticFile = result.Value;
        return new FileStreamResult(staticFile.Stream, staticFile.ContentType)
        {
            FileDownloadName = staticFile.FileName,
        };
    }
}