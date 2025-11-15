namespace Domain.Statistics.Weight;

public interface IWeightRecordsService
{
    Task AddOrUpdate(Guid userId, float weight, DateOnly date);
    Task<WeightStatistics> GetByPeriod(Guid userId, DateOnly from, DateOnly to);
}

public class WeightRecordsService(
    IWeightRecordsRepository weightRecordsRepository
) : IWeightRecordsService
{
    public async Task AddOrUpdate(Guid userId, float weight, DateOnly date)
    {
        await weightRecordsRepository.AddOrUpdate(userId, weight, date);
    }

    public async Task<WeightStatistics> GetByPeriod(Guid userId, DateOnly from, DateOnly to)
    {
        var ordered = await weightRecordsRepository.GetByPeriodIncluded(userId, from, to);
        return new(userId, ToRanges(ordered, from, to));
    }

    private WeightRecordRange[] ToRanges(WeightRecord[] weightRecords, DateOnly fromBorder, DateOnly toBorder)
    {
        var result = new WeightRecordRange[weightRecords.Length];
        for (var i = 0; i < weightRecords.Length; i++)
        {
            var (_, weight, date) = weightRecords[i];

            var from = i == 0
                       ? Min(date, fromBorder)
                       : weightRecords[i - 1].Date;
            var to = i == weightRecords.Length - 1 
                ? Max(date, toBorder) 
                : date;
            
            result[i] = new(weight, date, date);
        }

        return result;
    }
    
    private DateOnly Min(DateOnly first, DateOnly second) => first < second 
        ? first
        : second;
    
    private DateOnly Max(DateOnly first, DateOnly second) => first > second
        ? first
        : second;
}