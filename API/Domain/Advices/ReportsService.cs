#nullable enable

using Domain.Advices.DTO;
using Domain.Statistics.Calories;
using GigaChat;
using GigaChat.Completions.Request;
using Infrastructure.Results;

namespace Domain.Advices;

public interface IReportsService
{
    Task<Result<Report>> GetDailyReport(Guid userId, DateOnly currentDate);
    Task<Result<Report>> GetWeeklyReport(Guid userId, DateOnly currentDate);
}

public class ReportsService(
    IGigaChatClient gigaChatClient,
    IReportsGigaChatRequestGenerator reportsGigaChatRequestGenerator,
    INutrimentsStatisticsService nutrimentsStatisticsService
) : IReportsService
{
    public async Task<Result<Report>> GetDailyReport(Guid userId, DateOnly currentDate)
    {
        var statisticsDate = currentDate.AddDays(-1);
        var statistics = await nutrimentsStatisticsService.GetByPeriod(userId, statisticsDate, statisticsDate);
        return await GetReport(userId, statistics, false);
    }

    public async Task<Result<Report>> GetWeeklyReport(Guid userId, DateOnly currentDate)
    {
        var from = currentDate.AddDays(-7);
        var to = currentDate.AddDays(-1);
        var statistics = await nutrimentsStatisticsService.GetByPeriod(userId, from, to);
        return await GetReport(userId, statistics, false);
    }

    private async Task<Result<Report>> GetReport(Guid userId, NutrimentsStatistics statistics, bool isWeekly)
    {
        var gigaChatRequest = new GigaChatCompletionsRequest()
        {
            Messages = [
                new()
                {
                    Role = GigaChatCompletionsRequestMessageRole.System,
                    Content = reportsGigaChatRequestGenerator.CreateSystemMessage(statistics, isWeekly)
                },
                new ()
                {
                    Role = GigaChatCompletionsRequestMessageRole.User,
                    Content = reportsGigaChatRequestGenerator.CreateUserMessage(isWeekly)
                }
            ]
        };
        var response = await gigaChatClient.Completions(gigaChatRequest);

        return Results.Ok(new Report(response.Choices.First().Message.Content));
    }
}