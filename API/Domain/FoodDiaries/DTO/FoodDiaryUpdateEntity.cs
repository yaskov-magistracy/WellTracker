namespace Domain.FoodDiaries.DTO;

public record FoodDiaryUpdateEntity(
    ICollection<MealUpdateEntity>? Breakfast,
    ICollection<MealUpdateEntity>? Lunch,
    ICollection<MealUpdateEntity>? Snack,
    ICollection<MealUpdateEntity>? Dinner
);

public record MealUpdateEntity(
    Guid FoodId,
    int Grams
);