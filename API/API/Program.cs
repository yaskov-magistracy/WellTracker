using API.Configuration.Auth;
using API.Configuration.Swagger;
using API.Modules;
using DAL;
using Domain.Database.Github;
using Infrastructure.Config;
using Infrastructure.Configuration.Routes;
using Infrastructure.Configuration.Routes.ModelBinding;
using Infrastructure.Configuration.Serialization;
using TelegramBot;


var reader = new GitHubFileReader();
await reader.Read();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Config>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerConfiguration.Apply);

// TODO: to config/etc
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add(nameof(DateOnly), typeof(DateOnlyRouteConstraint));
});
builder.Services.AddControllers(opt =>
    {
        opt.ModelBinderProviders.Insert(0, new DateOnlyModelBinderProvider());
    })
    .AddJsonOptions(JsonConverters.ConfigureJson);
CookieAuth.Configure(builder.Services);

builder.Services.AddDbContext<DataContext>();
builder.Services.RegisterModules();


var app = builder.Build();

// if (Config.IsDev)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Services.GetRequiredService<TelegramDaemon>();

app.Run();