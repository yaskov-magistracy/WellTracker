using DAL.Foods;
using Domain.Foods;

namespace API.Modules.FoodsModule;

public class FoodsModule : IModule
{
    public void RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IFoodsRepository, FoodsRepository>();
        services.AddScoped<IFoodsService, FoodsService>();
    }
}