namespace FoodsParserV2.Models;

public class Nutriments
{
    public float? energy_kcal_100g { get; set; }
    public float? energy_100g { get; set; }
    public float? energy_kcal { get; set; }
    public float? energy { get; set; }
    public float? proteins_100g { get; set; }
    public float? proteins { get; set; }
    public float? fat_100g { get; set; }
    public float? fat { get; set; }
    public float? carbohydrates_100g { get; set; }
    public float? carbohydrates { get; set; }


    public bool IsIncorrect()
    {
        return (energy_kcal_100g == null && energy_kcal == null && energy_100g == null && energy == null)
               || (proteins_100g == null && proteins == null)
               || (fat_100g == null && fat == null)
               || (carbohydrates_100g == null && carbohydrates == null);
    }

    public void Normalize()
    {
        const float kcalCoeff = 4.184f;
        var apiKcal = energy_kcal_100g ?? energy_kcal;
        var apiKj = energy_100g ?? energy;

        var kcal = apiKcal ?? apiKj / kcalCoeff;
        var kj = apiKj ?? apiKcal * kcalCoeff;

        energy_kcal_100g = kcal;
        energy_kcal = kcal;
        energy_100g = kj;
        energy = kj;
    }
}