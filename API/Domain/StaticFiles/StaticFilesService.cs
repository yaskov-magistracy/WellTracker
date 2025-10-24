using Domain.StaticFiles.DTO;
using Infrastructure.Results;

namespace Domain.StaticFiles;

public interface IStaticFilesService
{
    Task<EmptyResult> Exists(Guid id);
    Task<Result<StaticFile>> Create(StaticFileCreateEntity createEntity);
    Task<Result<StaticFile>> Get(Guid id);
    Task<EmptyResult> Delete(Guid id);
}

public class StaticFilesService(
    IStaticFilesRepository staticFilesRepository
) : IStaticFilesService
{
    public async Task<EmptyResult> Exists(Guid id)
    {
        var existsInDb = await staticFilesRepository.Exists(id);
        if (!existsInDb)
            return EmptyResults.NotFound($"Файл с Id: {id} не существует в базе данных");

        return EmptyResults.NoContent();
    }

    public async Task<Result<StaticFile>> Create(StaticFileCreateEntity createEntity)
    {
        var file = createEntity.File;
        var created = await staticFilesRepository.Add(createEntity);

        return Results.Ok(new StaticFile(
            created.Id,
            file.FileName,
            file.ContentType,
            file.OpenReadStream()
        ));
    }

    public async Task<Result<StaticFile>> Get(Guid id)
    {
        var staticFile = await staticFilesRepository.GetById(id);
        if (staticFile == null)
            return Results.NotFound<StaticFile>($"Файла с Id: {id} не существует");
        
        return Results.Ok(staticFile);
    }

    public async Task<EmptyResult> Delete(Guid id)
    {
        var affectedInDb = await staticFilesRepository.Delete(id);
        if (affectedInDb == 0)
            return EmptyResults.NotFound($"Файла с Id: {id} не существует");

        await staticFilesRepository.Delete(id);
        return EmptyResults.NoContent();
    }
}