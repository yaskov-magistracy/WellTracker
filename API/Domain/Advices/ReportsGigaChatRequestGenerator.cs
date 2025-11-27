#nullable enable

using Domain.Statistics.Calories;

namespace Domain.Advices;

public interface IReportsGigaChatRequestGenerator
{
    public string CreateSystemMessage(NutrimentsStatistics statistics, bool isWeekly);
    public string CreateUserMessage(bool isWeekly);
}

public class ReportsGigaChatRequestGenerator(
    ) : IReportsGigaChatRequestGenerator
{
    public string CreateSystemMessage(NutrimentsStatistics statistics, bool isWeekly)
    {
        throw new NotImplementedException();
    }

    public string CreateUserMessage(bool isWeekly)
    {
        if (isWeekly)
        {
            return "Создай (еженедельное) уведомление для нашего пользователя. Дай оценку его прогрессу. Отметь его сильные и слабые стороны (если есть). " +
                   "\nДай пользователю рекомендации, если он сильно отклонился от плана питания. Сделай предположение о его физической активности за неделю на основе изменений и отклонения от плана. Напомни, что за дополнительными советами он может обратиться к тебе в самом приложении. " +
                   "\nНе забывай напомнить ему, что успех зависит от собственных усилий и привычек, а не только от рекомендаций нашего приложения." +
                   "\nЛимит символов: 1500.";
        }
        
        return
            "Создай (ежедневное) уведомление для нашего пользователя. Дай оценку его прогрессу. Отметь его сильные и слабые стороны (если есть). " +
            "\nДай пользователю рекомендации, если он сильно отклонился от плана питания. Напомни, что за дополнительными советами он может обратиться к тебе в самом приложении." +
            "\nНе забывай напомнить ему, что успех зависит от собственных усилий и ежедневных привычек, а не только от рекомендаций нашего приложения." +
            "\nЛимит символов: 600.";
    }
}