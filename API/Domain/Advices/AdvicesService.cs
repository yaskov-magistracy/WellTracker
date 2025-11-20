using Domain.Accounts.Users;
using Domain.Foods;
using Domain.Statistics.Calories;

namespace Domain.Advices;

public interface IAdvicesService
{
    Task<(FoodEnergy, FoodNutriments)> GetTargets(Guid userId);
    Task<(FoodEnergy, string[])> GetTargetAndAdvice(Guid userId, ICollection<NutrimentsRecord> records);
}

public class AdvicesService(
    IUsersService usersService
) : IAdvicesService
{
    public async Task<(FoodEnergy, FoodNutriments)> GetTargets(Guid userId)
    {
        var user = (await usersService.GetById(userId)).Value;
        return GetTargets(user);
    }

    public async Task<(FoodEnergy, string[])> GetTargetAndAdvice(Guid userId, ICollection<NutrimentsRecord> records)
    {
        var user = (await usersService.GetById(userId)).Value;
        var (targetEnergy, targetNutriments) = GetTargets(user);
        records = records.Where(e => e.IsNormal()).ToArray();
        var advice = await GetNutrimentsAdvice(records, targetNutriments);
        
        return (
            targetEnergy,
            advice
        );
    }

    private  (FoodEnergy, FoodNutriments) GetTargets(User user)
    {
        var targetKcal = user.Gender == UserGender.Male
            ? (15*user.TargetWeight) + (7*user.Height)
            : (14*user.TargetWeight) + (6*user.Height);
        var targetType = user.GetTargetType();
        var targetProtein = targetType switch
        {
            UserTargetType.Gain => user.Weight * 1.8f,
            UserTargetType.Keep => user.Weight * 1f,
            UserTargetType.Lose => user.Weight * 1.5f,
        };
        var targetFat = user.Weight * 0.9f;
        var targetCarbohydrates = CaloriesCalc.ToCarbohydrates(targetKcal
                                                                    - CaloriesCalc.FromProtein(targetProtein)
                                                                    - CaloriesCalc.FromFat(targetFat));
        return new(
            new FoodEnergy(targetKcal, CaloriesCalc.ToKj(targetKcal)),
            new FoodNutriments(targetProtein, targetFat, targetCarbohydrates));
    }

    public async Task<string[]> GetNutrimentsAdvice(
        ICollection<NutrimentsRecord> records,
        FoodNutriments targetNutriments)
    {
        var advices = new List<string>();
        var averageNutriments = records
            .Average(e => e.Nutriments);
        if (!averageNutriments.IsNormal())
            return ["Неполная статистика. Нельзя дать совет"];
        
        if (averageNutriments.Protein / targetNutriments.Protein > 1.7f)
            advices.Add("Ешьте меньше белка");
        if (averageNutriments.Protein / targetNutriments.Protein < 0.9f)
            advices.Add("Ешьте больше белка");
        if (averageNutriments.Fat / targetNutriments.Fat > 1.1f)
            advices.Add("Ешьте меньше жира");
        if (averageNutriments.Fat / targetNutriments.Fat < 0.9f)
            advices.Add("Ешьте больше жира");
        if (averageNutriments.Carbohydrates / targetNutriments.Carbohydrates > 1.1f)
            advices.Add("Ешьте меньше углеводов");
        if (averageNutriments.Carbohydrates / targetNutriments.Carbohydrates < 0.9f)
            advices.Add("Ешьте больше углеводов");

        if (!advices.Any())
            advices.Add("Ваше питание сбалансировано");

        return advices.ToArray();
    }
}