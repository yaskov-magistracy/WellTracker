using Domain.StaticFiles;
using Infrastructure.Results;
using Microsoft.AspNetCore.Http;

namespace DAL.StaticFiles;

public interface IStaticFilesProvider
{
    Task<bool> Exists(Guid fileKey);
    Task<EmptyResult> Create(IFormFile file, Guid key);
    Task<FileStream> GetStream(Guid fileKey);
    Task<bool> Delete(Guid fileKey);
}

public class LocalFilesProvider : IStaticFilesProvider, IStaticFilesCleaner
{
    private static readonly string PathToFiles = "./_staticFiles/";
    private string PathTo(Guid fileKey) => $"{PathToFiles}{fileKey}";

    public LocalFilesProvider()
    {
        if (!Directory.Exists(PathToFiles))
            Directory.CreateDirectory(PathToFiles);
    }

    public async Task<bool> Exists(Guid fileKey)
    {
        var path = PathTo(fileKey);
        return File.Exists(path);
    }

    public async Task<EmptyResult> Create(IFormFile file, Guid key)
    {
        try
        {
            await using var stream = file.OpenReadStream();

            await using var fileStream = File.Create($"{PathToFiles}{key}");
            await stream.CopyToAsync(fileStream);
        }
        catch (Exception e)
        {
            return EmptyResults.BadRequest(e.Message);
        }

        return EmptyResults.NoContent();
    }

    public async Task<FileStream> GetStream(Guid fileKey)
    {
        var path = PathTo(fileKey);
        if (!File.Exists(path))
            throw new ArgumentException($"Файл не существует в файловой системе. Path: {path}");
        
        return File.Open(path, FileMode.Open);
    }

    public async Task<bool> Delete(Guid fileKey)
    {
        var path = PathTo(fileKey);
        if (!File.Exists(path))
            throw new ArgumentException($"Файл не существует в файловой системе. Path: {path}");
        
        File.Delete(path);
        return true;
    }

    public void CleanUp()
    {
        Directory.Delete(PathToFiles, true);
        Directory.CreateDirectory(PathToFiles);
    }
}