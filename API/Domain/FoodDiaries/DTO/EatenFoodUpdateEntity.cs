namespace Domain.FoodDiaries.DTO;

public record EatenFoodUpdateEntity(
    Guid FoodId,
    int Grams
);