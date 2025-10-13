using API.Modules.StaticFilesModule.DTO;
using Domain.StaticFiles;
using Domain.StaticFiles.DTO;

namespace API.Modules.StaticFilesModule;

internal static class StaticFilesMapper
{
    public static StaticFileCreateEntity ToDomain(UploadFileRequest request)
        => new(request.File);

    public static UploadFileResponse ToResponse(StaticFile staticFile)
        => new(staticFile.Id, staticFile.FileName, staticFile.ContentType);
}