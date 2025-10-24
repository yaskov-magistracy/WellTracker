using Domain.StaticFiles;
using Domain.StaticFiles.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.StaticFiles;

public class StaticFilesRepository(
    DataContext dataContext,
    IStaticFilesProvider staticFilesProvider
) : IStaticFilesRepository
{
    private DbSet<StaticFileEntity> StaticFiles => dataContext.StaticFiles;
    
    public async Task<StaticFile?> GetById(Guid id)
    {
        var existed = await StaticFiles.FirstOrDefaultAsync(e => e.Id == id);
        if (existed == null)
            return null;

        return StaticFilesMapper.ToDomain(
            existed,
            await staticFilesProvider.GetStream(existed.Id));
    }

    public async Task<List<StaticFile>> Search()
    {
        var result = new List<StaticFile>();
        foreach (var e in StaticFiles)
        {
            var stream = await staticFilesProvider.GetStream(e.Id);
            result.Add(StaticFilesMapper.ToDomain(e, stream));
        }

        return result;
    }

    public async Task<bool> Exists(Guid id)
        => await StaticFiles.FirstOrDefaultAsync(e => e.Id == id) != null;

    public async Task<StaticFile> Add(StaticFileCreateEntity createEntity)
    {
        var newEntity = StaticFilesMapper.ToEntity(createEntity);
        await StaticFiles.AddAsync(newEntity);
        var createFileRes = await staticFilesProvider.Create(createEntity.File, newEntity.Id);
        if (!createFileRes.IsSuccess)
            throw new Exception(createFileRes.Error);

        await dataContext.SaveChangesAsync();
        return StaticFilesMapper.ToDomain(newEntity, createEntity.File.OpenReadStream());
    }

    public async Task<int> Delete(Guid id)
    {
        var affectedRow = await StaticFiles.Where(e => e.Id == id).ExecuteDeleteAsync();
        if (affectedRow == 1)
            await staticFilesProvider.Delete(id);
        
        return affectedRow;
    }
}