namespace Domain.Statistics.Weight;

public interface IWeightRecordsRepository
{
    Task AddOrUpdate(Guid userId, float weight, DateOnly date);
    Task<WeightRecord[]> GetByPeriodIncluded(Guid userId, DateOnly from, DateOnly to);
    Task<WeightDeviation> GetDeviationToCurrent(Guid userId, DateOnly fromDate);
}