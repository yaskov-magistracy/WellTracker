namespace Domain.StaticFiles;

public record class StaticFile(
    Guid Id,
    string FileName,
    string ContentType,
    Stream Stream)
{
}