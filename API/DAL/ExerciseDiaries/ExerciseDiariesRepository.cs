using DAL.Exercises;
using Domain.ExerciseDiaries;
using Domain.ExerciseDiaries.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.ExerciseDiaries;

public class ExerciseDiariesRepository(
    DataContext dataContext
) : IExerciseDiariesRepository
{
    private DbSet<ExerciseDiaryEntity> ExerciseDiaries => dataContext.ExerciseDiaries;

    private ExerciseDiaryRecord GetMockedTarget() => new(10000, null, new(0f));
    
    public async Task<ExerciseDiary?> GetByDate(Guid userId, DateOnly date)
    {
        var entity = await GetByDateInternal(userId, date);
        return entity != null
            ? ExerciseDiariesMapper.ToDomain(await GetExercises(entity), entity, GetMockedTarget())
            : null;
    }
    
    public async Task<bool> Exists(Guid userId, DateOnly date)
    {
        var entity = await GetByDateInternal(userId, date);
        return entity != null;
    }

    public async Task<ExerciseDiary> CreateOrUpdate(Guid userId, DateOnly date, ExerciseDiaryUpdateEntity updateEntity)
    {
        var entity = await GetByDateInternal(userId, date, asNoTracking: false);
        if (entity == null)
        {
            entity = new()
            {
                Date = date,
                UserId = userId,
                Current = new()
            };
            await UpdateEntity(entity, updateEntity);
            await ExerciseDiaries.AddAsync(entity);
        }
        else
        {
            await UpdateEntity(entity, updateEntity);
        }
        
        await dataContext.SaveChangesAsync();
        var exercisesById = await GetExercises(entity);
        return ExerciseDiariesMapper.ToDomain(exercisesById, entity, GetMockedTarget());
    }

    private async Task UpdateEntity(ExerciseDiaryEntity entity, ExerciseDiaryUpdateEntity updateEntity)
    {
        if (updateEntity.Steps != null)
            entity.Current.Steps = updateEntity.Steps.Value;
        if (updateEntity.Exercises != null)
            await UpdateCurrentExercises(entity, updateEntity);
    }

    private async Task UpdateCurrentExercises(ExerciseDiaryEntity entity, ExerciseDiaryUpdateEntity updateEntity)
    {
        entity.Current.CompletedExercises = updateEntity.Exercises!.Select(e => new CompletedExerciseEntity()
        {
            ExerciseId = e.ExerciseId,
            Repeats = e.Repeats,
            TimeInSeconds = e.TimeInSeconds,
        }).ToArray();
        entity.Current.Info = new()
        {
            TotalKcalBurnt = 0,
        };
    }

    private async Task<Dictionary<Guid, ExerciseEntity>> GetExercises(ExerciseDiaryEntity entity)
    {
        var exercisesIds = entity.Current.CompletedExercises == null
            ? []
            : entity.Current.CompletedExercises.Select(e => e.ExerciseId).ToArray();
        var exercises = dataContext.Exercises.AsNoTracking()
            .Where(e => exercisesIds.Contains(e.Id))
            .ToArray();
        return exercises.ToDictionary(e => e.Id, e => e);
    }

    private Task<ExerciseDiaryEntity?> GetByDateInternal(Guid userId, DateOnly date, bool asNoTracking = true)
    {
        var query = ExerciseDiaries.AsQueryable();
        if (asNoTracking)
            query = query.AsNoTracking();
        
        return query
            .Where(e => e.UserId == userId && e.Date == date)
            .FirstOrDefaultAsync();
    }
}