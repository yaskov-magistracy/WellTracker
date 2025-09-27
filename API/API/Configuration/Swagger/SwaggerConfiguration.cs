using System.Reflection;
using API.Configuration.Swagger.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Configuration.Swagger;

public static class SwaggerConfiguration
{
    public static void Apply(SwaggerGenOptions opt)
    {
        AddXmlComments(opt);
        opt.OperationFilter<AddAuthHeaderOperationFilter>();
    }

    private static void AddXmlComments(SwaggerGenOptions opt)
    {
        var xmlFilename = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
        opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
}