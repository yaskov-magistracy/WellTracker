using DAL.Accounts.Users;
using Domain.Accounts.Users;
using Domain.Advices;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace NotificationsWorker;

public interface INotificationSender
{
    Task SendDailyNotifications(DateOnly currentDate);
    Task SendWeeklyNotifications(DateOnly currentDate);
}

public class NotificationSender(
    IUsersRepository usersRepository,
    IReportsService reportsService,
    TelegramBotClient tgBotClient
) : INotificationSender
{
    public async Task SendDailyNotifications(DateOnly currentDate)
    {
        var users = await usersRepository.GetAll();
        foreach (var user in users)
        {
            if (user.TgChatId == null)
                continue;

            var report = await reportsService.GetDailyReport(user.Id, currentDate);
            await tgBotClient.SendMessage(user.TgChatId, report.Value.Text, ParseMode.Markdown);
        }
    }

    public async Task SendWeeklyNotifications(DateOnly currentDate)
    {
        var users = await usersRepository.GetAll();
        foreach (var user in users)
        {
            if (user.TgChatId == null)
                continue;

            var report = await reportsService.GetDailyReport(user.Id, currentDate);
            await tgBotClient.SendMessage(user.TgChatId, report.Value.Text, ParseMode.Markdown);
        }
    }
}