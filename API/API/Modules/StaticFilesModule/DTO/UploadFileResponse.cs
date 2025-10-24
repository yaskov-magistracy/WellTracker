namespace API.Modules.StaticFilesModule.DTO;

public record class UploadFileResponse(
    Guid FileId,
    string FileName,
    string ContentType)
{
    
}