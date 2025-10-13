using System.ComponentModel.DataAnnotations;

namespace DAL.StaticFiles;

public class StaticFileEntity
{
    [Key] public Guid Id { get; set; }
    public required string FileName { get; set; } = null!;
    public required string ContentType { get; set; } = null!;
}