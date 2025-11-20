namespace Domain.Statistics.Weight;

public record WeightStatistics(
    Guid UserId,
    ICollection<WeightRecordRange> Records)
{
    
}

public record WeightRecordRange(
    float Weight,
    DateOnly From,
    DateOnly To);

public record WeightRecord(
    Guid UserId,
    float Weight,
    DateOnly Date);