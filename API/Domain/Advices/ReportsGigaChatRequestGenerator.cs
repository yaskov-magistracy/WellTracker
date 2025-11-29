#nullable enable

using Domain.Accounts.Users;
using Domain.Statistics.Calories;
using Domain.Statistics.Weight;
using Newtonsoft.Json;

namespace Domain.Advices;

public interface IReportsGigaChatRequestGenerator
{
    public string CreateSystemMessage(User user, WeightDeviation weightDeviation, NutrimentsStatistics statistics, bool isWeekly);
    public string CreateUserMessage(bool isWeekly);
}

public class ReportsGigaChatRequestGenerator(
    ) : IReportsGigaChatRequestGenerator
{

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
    
    public string CreateSystemMessage(User user, WeightDeviation weightDeviation, NutrimentsStatistics statistics, bool isWeekly)
    {
        var (_, total, _, target, _) = statistics;
        var jsonMessage = JsonConvert.SerializeObject(new GigaChatReportSystemMessage(
            user.Login,
            user.Height,
            user.Gender.ToString(),
            isWeekly ? "Week" : "Day",
            new(weightDeviation.Current, weightDeviation.Target, weightDeviation.DeviationAbsolute, weightDeviation.DeviationRelative),
            new(total.Energy.Kcal, target.Energy.Kcal),
            new(total.Nutriments.Protein, target.Nutriments.Protein),
            new(total.Nutriments.Fat, target.Nutriments.Fat),
            new(total.Nutriments.Carbohydrates, target.Nutriments.Carbohydrates)));

        return $"{jsonMessage}\n{SystemJsonDescriptionMessage}";
    }

    private record GigaChatReportSystemMessage(
        string Name,
        int Height,
        string GenderString,
        string Period,
        ReportParam Weight,
        ReportParam Kcal,
        ReportParam Protein,
        ReportParam Fat,
        ReportParam Carbohydrates)
    {
    }

    private class ReportParam
    {
        public ReportParam(
            float current,
            float target,
            float? deviationAbsolute = null,
            float? deviationRelative = null)
        {
            Current = current;
            Target = target;
            deviationAbsolute ??= current - target;
            DeviationAbsolute = deviationAbsolute.Value;
            DeviationRelative = deviationAbsolute.Value / target * 100f;
        }
        
        public float Current { get; set; }
        public float Target { get; set; }
        public float DeviationAbsolute { get; set; }
        public float DeviationRelative { get; set; }
    }

    private const string SystemJsonDescriptionMessage = @"
Пояснения:
`Рeight`, `пender`: базовые характеристики пользователя.
Period - определяет за какой период определяются все параметры ниже.
Группа параметров ""Цeight"": объект, включающий четыре параметра:
""Сurrent"" — текущий вес пользователя.
""Target"" — целевой вес по плану питания.
""DeviationAbsolute"" — изменение веса за Period в абсолютной величине.
""DeviationRelative"" — изменение веса за Period в относительной величине.

В остальных группах 
""DeviationAbsolute"" — изменение парамтера относительно плановых показателей за Period в абсолютной величине.
""DeviationRelative"" — изменение параметра относительно плановых показателей за Period в относительной величине.
Группа ""Kcal"" - то же что ""weight"", но по каллориям
Группа ""Protein"" - то же что ""weight"", но по белкам
Группа ""Fat"" - то же что ""weight"", но по жирам
Группа ""Carbohydrates"" - то же что ""weight"", но по углеводам
";


}