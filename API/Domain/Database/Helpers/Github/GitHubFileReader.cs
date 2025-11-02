using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using CsvHelper;

namespace Domain.Database.Github;

public interface IGitHubFileReader
{
    Task<(CsvReader, IEnumerable<T>)> Read<T>();
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
    
    public async Task<(CsvReader, IEnumerable<T>)> Read<T>()
    {
        var url = $"{BaseUrl}/repos/yaskov-magistracy/WellTracker/contents/API.Tools/FoodsParserV2/foods.csv";
        var request = new HttpRequestMessage(
            HttpMethod.Get, 
            url);
        var gitHttpResponse = await httpClient.SendAsync(request);
        var gitResponse = await gitHttpResponse.Content.ReadFromJsonAsync<GitHubResponse>();
        var csvResponse = await httpClient.GetAsync(gitResponse!.DownloadUrl);
        var stream = await csvResponse.Content.ReadAsStreamAsync();
        var reader = new StreamReader(stream);
        var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        return (csvReader, csvReader.GetRecords<T>());
    }

    class GitHubResponse
    {
        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; }
    }
}