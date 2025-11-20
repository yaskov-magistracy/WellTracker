namespace Domain.Foods;

public static class FoodEnergyExtensions
{
    public static FoodEnergy Sum<T>(this IEnumerable<T> foodEnergyList, Func<T, FoodEnergy> selector)
    {
        var result = new FoodEnergy(0, 0);
        foreach (var foodEnergy in foodEnergyList)
        {
            var current = selector(foodEnergy);
            result = result with
            {
                Kcal = result.Kcal + current.Kcal,
                Kj = result.Kj + current.Kj,
            };
        }

        return result;
    }
    
    public static FoodEnergy Average<T>(this IEnumerable<T> foodEnergyList, Func<T, FoodEnergy> selector)
    {
        var count = 0;
        var result = new FoodEnergy(0, 0);
        foreach (var foodEnergy in foodEnergyList)
        {
            count++;
            var current = selector(foodEnergy);
            result = result with
            {
                Kcal = result.Kcal + current.Kcal,
                Kj = result.Kj + current.Kj,
            };
        }

        count = count == 0 ? 1 : count;
        return new FoodEnergy(
            result.Kcal / count,
            result.Kj / count);
    }
}