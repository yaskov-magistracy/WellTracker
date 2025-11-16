using Domain.Accounts.Users;
using Domain.Advices;
using Domain.FoodDiaries;
using Domain.Foods;

namespace Domain.Statistics.Calories;

public interface INutrimentsStatisticsService
{
    Task<NutrimentsStatistics> GetByPeriod(Guid userId, DateOnly from, DateOnly to);
}

public class NutrimentsStatisticsService(
    IUsersService usersService,
    IFoodDiariesRepository foodDiariesRepository,
    IAdvicesService advicesService
    ) : INutrimentsStatisticsService
{
    public async Task<NutrimentsStatistics> GetByPeriod(Guid userId, DateOnly from, DateOnly to)
    {
        var records = (await foodDiariesRepository.GetByRange(userId, from, to))
            .Select(e => new NutrimentsRecord(e.Date, e.TotalEnergy, e.TotalNutriments))
            .ToArray();
        var averageEnergy = records.Average(e => e.Energy);
        var (targetEnergy, advices) = await advicesService.GetTargetAndAdvice(userId, records);

        return new NutrimentsStatistics(
            records,
            averageEnergy,
            targetEnergy,
            advices);
    }
}