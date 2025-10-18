using System.ComponentModel;

namespace API.ApiInfrasturcture.DTO.Search;

public abstract class BaseApiSearchRequest
{
    [DefaultValue(100)]
    public int Take { get; set; } = 100;
    [DefaultValue(0)]
    public int Skip { get; set; } = 0;
}