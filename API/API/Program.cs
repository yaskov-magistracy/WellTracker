using API.Configuration.Auth;
using API.Configuration.Swagger;
using API.Modules;
using DAL;
using Infrastructure.Config;
using Infrastructure.Configuration.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Config>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerConfiguration.Apply);

builder.Services.AddControllers()
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

app.Run();