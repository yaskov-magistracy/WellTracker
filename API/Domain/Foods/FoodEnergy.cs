namespace Domain.Foods;

public record FoodEnergy(
    float Kcal,
    float Kj)
{
    public static FoodEnergy Empty() =>  new FoodEnergy(0f, 0f);
}