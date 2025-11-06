using DAL.ExerciseDiaries;
using Domain.ExerciseDiaries;

namespace API.Modules.ExerciseDiariesModule;

public class ExerciseDiaryModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IExerciseDiariesRepository, ExerciseDiariesRepository>();
        services.AddScoped<IExerciseDiariesService, ExerciseDiaryService>();
    }
}