using Domain.Statistics.Weight;
using Microsoft.EntityFrameworkCore;

namespace DAL.Statistics.Weight;

public class WeightRecordsRepository(
    DataContext dataContext
    ) : IWeightRecordsRepository
{
    private DbSet<WeightRecordEntity> WeightRecords => dataContext.WeightRecords;
    
    public async Task AddOrUpdate(Guid userId, float weight, DateOnly date)
    {
        var existed = await WeightRecords.FirstOrDefaultAsync(e => e.UserId == userId && e.Date == date);
        if (existed != null)
        {
            existed.Weight = weight;
        }
        else
        {
            var toAdd = new WeightRecordEntity()
            {
                UserId = userId,
                Date = date,
                Weight = weight
            };
            await WeightRecords.AddAsync(toAdd);
        }
        
        await dataContext.SaveChangesAsync();
    }

    public async Task<WeightRecord[]> GetByPeriodIncluded(Guid userId, DateOnly from, DateOnly to)
    {
        var records = WeightRecords
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .Where(e => e.Date >= from && e.Date <= to)
            .OrderBy(e => e.Date);

        return records
            .Select(WeightRecordsMapper.ToDomain)
            .ToArray();
    }
}