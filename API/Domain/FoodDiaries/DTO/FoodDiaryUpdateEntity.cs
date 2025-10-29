namespace Domain.FoodDiaries.DTO;

public record FoodDiaryUpdateEntity(
    ICollection<EatenFoodUpdateEntity>? Breakfast,
    ICollection<EatenFoodUpdateEntity>? Lunch,
    ICollection<EatenFoodUpdateEntity>? Snack,
    ICollection<EatenFoodUpdateEntity>? Dinner
);

public record EatenFoodUpdateEntity(
    Guid FoodId,
    int Grams
);