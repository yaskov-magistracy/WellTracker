using Domain.StaticFiles.DTO;

namespace Domain.StaticFiles;

public interface IStaticFilesRepository
{
    Task<StaticFile?> GetById(Guid id);
    Task<List<StaticFile>> Search();
    Task<bool> Exists(Guid id);
    Task<StaticFile> Add(StaticFileCreateEntity createEntity);
    Task<int> Delete(Guid id);
}