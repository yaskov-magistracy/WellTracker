namespace Domain.Foods;

public static class FoodNutrimentsExtensions
{
    public static FoodNutriments Sum<T>(this IEnumerable<T> list, Func<T, FoodNutriments> selector)
    {
        var result = new FoodNutriments(0, 0, 0);
        foreach (var e in list)
        {
            var curNutriments = selector(e);
            result = result with
            {
                Protein = result.Protein + curNutriments.Protein,
                Fat = result.Fat + curNutriments.Fat,
                Carbohydrates = result.Carbohydrates + curNutriments.Carbohydrates,
            };
        }

        return result;
    }

    public static FoodNutriments Average<T>(this IEnumerable<T> list, Func<T, FoodNutriments> selector)
    {
        var count = 0;
        var result = new FoodNutriments(0, 0, 0);
        foreach (var e in list)
        {
            count++;
            var curNutriments = selector(e);
            result = result with
            {
                Protein = result.Protein + curNutriments.Protein,
                Fat = result.Fat + curNutriments.Fat,
                Carbohydrates = result.Carbohydrates + curNutriments.Carbohydrates,
            };
        }

        count = count == 0 ? 1 : count;
        return new FoodNutriments(
            Protein: result.Protein / count, 
            Fat: result.Fat / count, 
            Carbohydrates: result.Carbohydrates / count);
    }
}