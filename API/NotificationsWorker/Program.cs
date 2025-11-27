using NotificationsWorker;

var builder = Host.CreateApplicationBuilder(args);
// DAL.Dependencies.Register(builder.Services);
builder.Services.AddHostedService<Worker>();


var host = builder.Build();
host.Run();