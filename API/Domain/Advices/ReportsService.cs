#nullable enable

using Domain.Accounts.Users;
using Domain.Advices.DTO;
using Domain.Statistics.Calories;
using Domain.Statistics.Weight;
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
    INutrimentsStatisticsService nutrimentsStatisticsService,
    IWeightRecordsRepository weightRecordsRepository,
    IUsersRepository usersRepository
) : IReportsService
{
    public async Task<Result<Report>> GetDailyReport(Guid userId, DateOnly currentDate)
    {
        var statisticsDate = currentDate.AddDays(-1);
        var statistics = await nutrimentsStatisticsService.GetByPeriod(userId, statisticsDate, statisticsDate);
        if (statistics.Records.Count == 0)
            return Results.Ok(new Report($"Я хотел проанализировать твои результаты за вчера, но к сожалению ты ничего не записал("));
            
        var weightDeviation = await weightRecordsRepository.GetDeviationToCurrent(userId, statisticsDate);
        return await GetReport(userId, weightDeviation, statistics, false);
    }

    public async Task<Result<Report>> GetWeeklyReport(Guid userId, DateOnly currentDate)
    {
        var from = currentDate.AddDays(-7);
        var to = currentDate.AddDays(-1);
        var statistics = await nutrimentsStatisticsService.GetByPeriod(userId, from, to);
        if (statistics.Records.Count < 5)
            return Results.Ok(new Report($"Я хотел проанализировать твои результаты за неделю, но мне не хватило данных("));
        
        var weightDeviation = await weightRecordsRepository.GetDeviationToCurrent(userId, from);
        return await GetReport(userId, weightDeviation, statistics, false);
    }

    private async Task<Result<Report>> GetReport(Guid userId, WeightDeviation weightDeviation, NutrimentsStatistics statistics, bool isWeekly)
    {
        var user = await usersRepository.Get(userId);
        var gigaChatRequest = new GigaChatCompletionsRequest()
        {
            Messages = [
                new()
                {
                    Role = GigaChatCompletionsRequestMessageRole.System,
                    Content = reportsGigaChatRequestGenerator.CreateSystemMessage(user!, weightDeviation, statistics, isWeekly)
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