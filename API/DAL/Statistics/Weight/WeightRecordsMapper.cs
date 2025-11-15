using Domain.Statistics.Weight;

namespace DAL.Statistics.Weight;

internal static class WeightRecordsMapper
{
    public static WeightRecord ToDomain(WeightRecordEntity entity)
        => new(entity.UserId, entity.Weight, entity.Date);
}