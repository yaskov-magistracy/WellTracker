using System.ComponentModel.DataAnnotations.Schema;
using DAL.Accounts.Users;
using DAL.Foods;

namespace DAL.FoodDiaries;

internal class FoodDiaryEntity
{
    public Guid Id { get; set; }
    [ForeignKey(nameof(User))] public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public DateOnly Date { get; set; }
    public ICollection<MealEntity> Breakfast { get; set; }
    public ICollection<MealEntity> Lunch { get; set; }
    public ICollection<MealEntity> Snack { get; set; }
    public ICollection<MealEntity> Dinner { get; set; }
    public FoodNutrimentsEntity TotalNutriments { get; set; }
    public FoodEnergyEntity TotalEnergy { get; set; }
}

internal class MealEntity
{
    public FoodEntity Food { get; set; } // TODO: В идеале нужно сократить тут сущность до реально необходимых полей
    public int Grams { get; set; }
}