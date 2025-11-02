using Domain.Foods;

namespace Domain.FoodDiaries;

public record FoodDiary(
    Guid Id,
    Guid UserId,
    DateOnly Date,
    Meal? Breakfast,
    Meal? Lunch,
    Meal? Snack,
    Meal? Dinner,
    FoodNutriments TotalNutriments,
    FoodEnergy TotalEnergy
);

public record Meal(
    ICollection<EatenFood> EatenFoods,
    FoodNutriments TotalNutriments,
    FoodEnergy TotalEnergy
);

public record EatenFood(
    Food Food,
    int Grams
);