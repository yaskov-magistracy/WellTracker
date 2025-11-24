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
        var total = new FoodValue(
            records.Sum(e => e.Nutriments),
            records.Sum(e => e.Energy));
        var average = new FoodValue(
            records.Average(e => e.Nutriments),
            records.Average(e => e.Energy));
        var (target, advices) = await advicesService.GetTargetsAndAdvice(userId, records);

        return new NutrimentsStatistics(
            records,
            total,
            average,
            target,
            advices);
    }
}