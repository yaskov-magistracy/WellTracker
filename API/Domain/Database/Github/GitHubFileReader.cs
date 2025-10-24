using System.Net.Http.Headers;

namespace Domain.Database.Github;

public interface IGitHubFileReader
{
    Task Read();
}

public class GitHubFileReader : IGitHubFileReader
{
    private readonly HttpClient httpClient;
    private const string BaseUrl = "https://api.github.com";

    public GitHubFileReader()
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "C#-App");
    }
    
    public async Task Read()
    {
        var url = $"{BaseUrl}/repos/yaskov-magistracy/WellTracker/contents/API.Tools/FoodsParser/foods.csv";
        var request = new HttpRequestMessage(
            HttpMethod.Get, 
            url);
        var response = await httpClient.SendAsync(request);
    }
}