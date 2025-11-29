using NotificationsWorker;

var builder = Host.CreateApplicationBuilder(args);
// DAL.Dependencies.Register(builder.Services);
builder.Services.AddHostedService<Worker>();
DAL.Dependencies.Register(builder.Services);
builder.Services.AddScoped<INotificationSender, NotificationSender>();

var host = builder.Build();
host.Run();