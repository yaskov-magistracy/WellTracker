using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NpgsqlTypes;

namespace DAL.Foods;

public class FoodEntity
{
    [Key] public Guid Id { get; set; }
    [JsonIgnore] public NpgsqlTsVector SearchVector { get; set; }
    public string Name { get; set; }
    public string? BrandName { get; set; }
    public float? GramsInPortion { get; set; }
    public FoodNutrimentsEntity Nutriments { get; set; }
    public FoodEnergyEntity Energy { get; set; }
}

[Owned]
public class FoodNutrimentsEntity
{
    public float Protein { get; set; }
    public float Fat { get; set; }
    public float Сarbohydrates { get; set; }
}

[Owned]
public class FoodEnergyEntity
{
    public float Kcal { get; set; }
    public float Kj { get; set; }
}