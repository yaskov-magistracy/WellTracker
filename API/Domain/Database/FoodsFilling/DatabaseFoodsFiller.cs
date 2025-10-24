using Domain.Foods;

namespace Domain.Database.FoodsFilling;

public interface IDatabaseFoodsFiller
{
    
}

public class DatabaseFoodsFiller(
    IFoodsService foodsService
) : IDatabaseFoodsFiller
{
    
}