using DAL.StaticFiles;
using Domain.StaticFiles;

namespace API.Modules.StaticFilesModule;

public class StaticFilesModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddSingleton<IStaticFilesProvider, LocalFilesProvider>();
        services.AddSingleton<IStaticFilesCleaner, LocalFilesProvider>();
        services.AddScoped<IStaticFilesRepository, StaticFilesRepository>();
        services.AddScoped<IStaticFilesService, StaticFilesService>();
    }
}