namespace Domain.Statistics;

public interface IStatisticsService
{
    
}

public class StatisticsService(
    IStatisticsRepository statisticsRepository
    ) : IStatisticsService
{
    
}