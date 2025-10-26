using DAL;
using Domain.Database;
using Domain.Database.FoodsFilling;
using Domain.Database.Github;

namespace API.Modules.DatabaseModule;

public class DatabaseModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddSingleton<IGitHubFileReader, GitHubFileReader>();
        services.AddScoped<IDatabaseFoodsFiller, DatabaseFoodsFiller>();
        services.AddScoped<IDatabaseService, DatabaseService>();
        services.AddScoped<IDatabaseAccessor, DatabaseAccessor>();
    }
}