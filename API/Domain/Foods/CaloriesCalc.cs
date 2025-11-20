namespace Domain.Foods;

public static class CaloriesCalc
{
    private const float Coefficient = 4.1868f;
    
    public static float ToKcal(float kj)
    {
        return kj * Coefficient;
    }

    public static float ToKj(float kcal)
    {
        return kcal / Coefficient;
    }
    
    public static float FromProtein(float proteins)
        => proteins * 4f;
    
    public static float ToProtein(float kcal)
        => kcal / 4f;

    public static float FromFat(float fats)
        => fats * 9f;
    
    public static float ToFat(float kcal)
        => kcal / 9f;
    
    public static float FromCarbohydrates(float carbohydrates)
        => carbohydrates * 4f;
    
    public static float ToCarbohydrates(float kcal)
        => kcal / 4f;
}