namespace Domain.Foods;

public record FoodNutriments(
    float Protein,
    float Fat,
    float Carbohydrates)
{
    public bool IsNormal()
    {
        return Protein != 0 && Fat != 0 && Carbohydrates != 0;
    }
}