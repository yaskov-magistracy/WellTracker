using Domain.Statistics.Weight;

namespace API.Modules.StatisticsModule.DTO;

public record WeightStatisticsApiResponse(
    float CurrentWeight,
    float TargetWeight,
    WeightStatistics Statistics)
{
    
}