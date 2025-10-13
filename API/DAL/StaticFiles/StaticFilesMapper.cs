using Domain.StaticFiles;
using Domain.StaticFiles.DTO;

namespace DAL.StaticFiles;

internal static class StaticFilesMapper
{
    public static StaticFile ToDomain(StaticFileEntity entity, Stream stream)
        => new(entity.Id, entity.FileName, entity.ContentType, stream);

    public static StaticFileEntity ToEntity(StaticFileCreateEntity createEntity)
        => new()
        {
            FileName = createEntity.File.FileName,
            ContentType = createEntity.File.ContentType,
        };
}