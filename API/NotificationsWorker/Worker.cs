namespace NotificationsWorker;

public class Worker(
    IServiceProvider serviceProvider,
    ILogger<Worker> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            var ekbDateTime = DateTime.UtcNow.AddHours(5);
            var isDaily = IsDailyNotification(ekbDateTime);
            var isWeekly = IsWeeklyNotification(ekbDateTime);
            if (isDaily || isWeekly)
            {
                var date = DateOnly.FromDateTime(ekbDateTime);
                using var scope = serviceProvider.CreateScope();
                var notificationsSender = scope.ServiceProvider.GetRequiredService<INotificationSender>();
                if (isDaily)
                    await notificationsSender.SendDailyNotifications(date);
                if (isWeekly)
                    await notificationsSender.SendWeeklyNotifications(date);
            }
            await Task.Delay(TimeSpan.FromHours(1));
        }
    }

    private bool IsDailyNotification(DateTime ekbDateTime)
    {
        return ekbDateTime.Hour == 10;
    }

    private bool IsWeeklyNotification(DateTime ekbDateTime)
    {
        return ekbDateTime is { DayOfWeek: DayOfWeek.Sunday, 
            Hour: 17 };
    }
}