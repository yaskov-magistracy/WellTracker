using Domain.Foods;
using Domain.Foods.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Foods;

public class FoodsRepository(
    DataContext dataContext
) : IFoodsRepository
{
    private DbSet<FoodEntity> Foods => dataContext.Foods;

    public async Task<FoodSearchResponse> Search(FoodSearchRequest request)
    {
        var query = Foods
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(e => e.SearchVector.Matches(request.SearchText));

        return new(
            query.Skip(request.Skip).Take(request.Take).AsEnumerable().Select(FoodsMapper.ToDomain).ToArray(),
            await query.CountAsync());

    }

    public async Task<Food?> Get(Guid foodId)
    {
        var entity = await Foods.AsNoTracking().FirstOrDefaultAsync(x => x.Id == foodId);
        return entity != null
            ? FoodsMapper.ToDomain(entity)
            : null;
    }

    public async Task<bool> Exists(Guid foodId)
    {
        var entity = await Foods.AsNoTracking().FirstOrDefaultAsync(e => e.Id == foodId);
        return entity != null;
    }

    public async Task AddBatch(ICollection<FoodCreateEntity> createEntities)
    {
        var entities = createEntities.Select(FoodsMapper.ToEntity);
        await Foods.AddRangeAsync(entities);
        await dataContext.SaveChangesAsync();
    }

    public async Task<Food> Add(FoodCreateEntity createEntity)
    {
        var entity = FoodsMapper.ToEntity(createEntity);
        await Foods.AddAsync(entity);
        await dataContext.SaveChangesAsync();
        return FoodsMapper.ToDomain(entity);
    }

    public async Task<Food> Update(Guid foodId, FoodUpdateEntity updateEntity)
    {
        var food = await Foods.FirstAsync(e => e.Id == foodId);
        if (updateEntity.Name != null)
            food.Name = updateEntity.Name;
        if (updateEntity.BrandName != null)
            food.BrandName = updateEntity.BrandName.Value;
        if (updateEntity.GramsInPortion != null)
            food.GramsInPortion = updateEntity.GramsInPortion.Value;
        if (updateEntity.Nutriments?.Fat != null)
            food.Nutriments.Fat = updateEntity.Nutriments.Fat.Value;
        if (updateEntity.Nutriments?.Protein != null)
            food.Nutriments.Protein = updateEntity.Nutriments.Protein.Value;
        if (updateEntity.Nutriments?.Сarbohydrates != null)
            food.Nutriments.Сarbohydrates = updateEntity.Nutriments.Сarbohydrates.Value;
        if (updateEntity.Energy?.Kcal != null)
            food.Energy.Kcal = updateEntity.Energy.Kcal.Value;
        if (updateEntity.Energy?.Kj != null)
            food.Energy.Kj = updateEntity.Energy.Kj.Value;
        
        await dataContext.SaveChangesAsync();
        return FoodsMapper.ToDomain(food);
    }
}