using Microsoft.AspNetCore.Http;

namespace Domain.StaticFiles.DTO;

public record class StaticFileCreateEntity(
    IFormFile File)
{
}