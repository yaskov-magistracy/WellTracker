using API.Modules.ExercisesModule.DTO;
using Domain.Exercises.DTO;

namespace API.Modules.ExercisesModule;

internal static class ExercisesMapper
{
    public static ExerciseSearchRequest ToDomain(ExerciseSearchApiRequest request)
        => new(request.SearchText,
            request.Type,
            request.Muscles,
            request.Equipments,
            request.Measurement,
            request.Take,
            request.Skip);
}