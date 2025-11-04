using DAL.Exercises.Models;
using Domain.Exercises;
using Domain.Exercises.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Exercises;

public class ExercisesRepository(
    DataContext dataContext
) : IExercisesRepository
{
    private DbSet<ExerciseEntity> Exercises => dataContext.Exercises;
    private DbSet<ExerciseEntity> NotIncludedExercises => dataContext.Exercises;
    
    public async Task<ExerciseSearchResponse> Search(ExerciseSearchRequest request)
    {
        var query = Exercises.AsNoTracking()
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(e => e.SearchVector.Matches(request.SearchText));
        if (request.Type != null)
            query = query.Where(e => e.Type == ExercisesMapper.ToEntity(request.Type.Value));
        if (request.Measurement != null)
            query = query.Where(e => e.Measurement == ExercisesMapper.ToEntity(request.Measurement.Value));
        if (request.Muscles is {Length: > 0})
            query = query.Where(e => e.Muscles.Intersect(request.Muscles.Select(ExercisesMapper.ToEntity)).Count() == request.Muscles.Length);
        if (request.Equipments is {Length: > 0})
            query = query.Where(e => e.Equipments.Intersect(request.Equipments.Select(ExercisesMapper.ToEntity)).Count() == request.Equipments.Length);

        return new(
            query.Skip(request.Skip).Take(request.Take).AsEnumerable().Select(ExercisesMapper.ToDomain).ToArray(),
            await query.CountAsync());
    }

    public async Task<Exercise?> Get(Guid exerciseId)
    {
        var entity = await Exercises.AsNoTracking().FirstOrDefaultAsync(e => e.Id == exerciseId);
        return entity != null
            ? ExercisesMapper.ToDomain(entity)
            : null;
    }

    public async Task<bool> Exists(Guid exerciseId)
    {
        var entity = await NotIncludedExercises.AsNoTracking().FirstOrDefaultAsync(e => e.Id == exerciseId);
        return entity != null;
    }

    public async Task AddBatch(ICollection<ExerciseCreateEntity> createEntities)
    {
        var entities = createEntities.Select(ExercisesMapper.ToEntity);
        await NotIncludedExercises.AddRangeAsync(entities);
        await dataContext.SaveChangesAsync();
    }

    public async Task<Exercise> Add(ExerciseCreateEntity createEntity)
    {
        var entity = ExercisesMapper.ToEntity(createEntity);
        NotIncludedExercises.Add(entity);
        await dataContext.SaveChangesAsync();
        return ExercisesMapper.ToDomain(entity);
    }

    public async Task<Exercise> Update(Guid exerciseId, ExerciseUpdateEntity updateEntity)
    {
        var exercise = await NotIncludedExercises.FirstAsync(e => e.Id == exerciseId);
        if (updateEntity.Name != null)
            exercise.Name = updateEntity.Name;
        if (updateEntity.Description != null)
            exercise.Description = updateEntity.Description;
        if (updateEntity.Type != null)
            exercise.Type = ExercisesMapper.ToEntity(updateEntity.Type.Value);
        if (updateEntity.Measurement != null)
            exercise.Measurement = ExercisesMapper.ToEntity(updateEntity.Measurement.Value);
        if (updateEntity.Muscles is {Length: > 0})
            exercise.Muscles = updateEntity.Muscles.Select(ExercisesMapper.ToEntity).ToArray();
        if (updateEntity.Equipments is {Length: > 0})
            exercise.Equipments = updateEntity.Equipments.Select(ExercisesMapper.ToEntity).ToArray();
        
        await dataContext.SaveChangesAsync();
        return ExercisesMapper.ToDomain(exercise);
    }
}