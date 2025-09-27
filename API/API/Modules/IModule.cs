namespace API.Modules;

public interface IModule
{
    void RegisterModule(IServiceCollection services);
}

public static class ModuleExtensions
{
    private static List<IModule>? _modules;
    private static List<IModule> Modules => _modules ??= DiscoverModules().ToList();

    public static void RegisterModules(this IServiceCollection services)
    {
        foreach (var module in Modules)
        {
            module.RegisterModule(services);
        }
    }

    private static IEnumerable<IModule> DiscoverModules()
    {
        return typeof(IModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
    }
}