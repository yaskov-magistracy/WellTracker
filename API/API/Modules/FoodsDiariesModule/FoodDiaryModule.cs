using DAL.FoodDiaries;
using Domain.FoodDiaries;

namespace API.Modules.FoodsDiariesModule;

public class FoodDiaryModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IFoodDiariesRepository, FoodDiariesRepository>();
        services.AddScoped<IFoodDiariesService, FoodDiariesService>();
    }
}