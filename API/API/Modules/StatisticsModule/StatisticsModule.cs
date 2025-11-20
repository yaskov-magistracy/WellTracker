using DAL.Statistics.Weight;
using Domain.Advices;
using Domain.Statistics.Calories;
using Domain.Statistics.Weight;

namespace API.Modules.StatisticsModule;

public class StatisticsModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IWeightRecordsRepository, WeightRecordsRepository>();
        services.AddScoped<IWeightRecordsService, WeightRecordsService>();
        services.AddScoped<INutrimentsStatisticsService, NutrimentsStatisticsService>();
        services.AddScoped<IAdvicesService, AdvicesService>();
    }
}