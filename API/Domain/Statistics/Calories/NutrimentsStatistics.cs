using Domain.Foods;

namespace Domain.Statistics.Calories;

public record NutrimentsStatistics(
    ICollection<NutrimentsRecord> Records,
    FoodValue Total,
    FoodValue Average,
    FoodValue Target,
    string[] Advices
    )
{
    
}

public record NutrimentsRecord(
    DateOnly Date,
    FoodEnergy Energy,
    FoodNutriments Nutriments)
{
    public bool IsNormal()
    {
        return Nutriments.IsNormal();
    }
}