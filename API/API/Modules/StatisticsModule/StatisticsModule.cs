using DAL.Statistics.Weight;
using Domain.Statistics.Weight;

namespace API.Modules.StatisticsModule;

public class StatisticsModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IWeightRecordsRepository, WeightRecordsRepository>();
        services.AddScoped<IWeightRecordsService, WeightRecordsService>();
    }
}