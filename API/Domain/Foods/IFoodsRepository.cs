using Domain.Foods.DTO;

namespace Domain.Foods;

public interface IFoodsRepository
{
    Task<FoodSearchResponse> Search(FoodSearchRequest request);
    Task<Food?> Get(Guid foodId);
    Task<bool> Exists(Guid foodId);
    Task AddBatch(ICollection<FoodCreateEntity> createEntities);
    Task<Food> Add(FoodCreateEntity createEntity);
    Task<Food> Update(Guid foodId, FoodUpdateEntity updateEntity);
}