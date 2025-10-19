using Domain.Foods;

namespace Domain.FoodDiaries;

public record FoodDiary(
    Guid Id,
    Guid UserId,
    DateOnly Date,
    ICollection<Meal> Breakfast,
    ICollection<Meal> Lunch,
    ICollection<Meal> Snack,
    ICollection<Meal> Dinner,
    FoodNutriments TotalNutriments,
    FoodEnergy TotalEnergy
);

public record Meal(
    Food Food,
    int Grams
);