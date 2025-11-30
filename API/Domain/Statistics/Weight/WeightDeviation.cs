namespace Domain.Statistics.Weight;

public record WeightDeviation(
    float Current,
    float Target,
    float DeviationAbsolute,
    float DeviationRelative)
{
    
}