using DAL.Exercises;
using Domain.Exercises;

namespace API.Modules.ExercisesModule;

public class ExercisesModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IExercisesRepository, ExercisesRepository>();
        services.AddScoped<IExercisesService, ExercisesService>();
    }
}