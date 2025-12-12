using Domain.Accounts.Users;
using Domain.FoodDiaries;
using Domain.Foods;
using GigaChat.Completions.Request;

namespace Domain.Chats;

public interface IChatContextProvider
{
    Task<IList<GigaChatCompletionsRequestMessage>> GetChatContext(Guid chatId, Guid userId);
}

public class ChatContextProvider(
    IChatsRepository chatsRepository,
    IUsersRepository usersRepository,
    IFoodDiariesService foodDiariesService
) : IChatContextProvider
{
    public async Task<IList<GigaChatCompletionsRequestMessage>> GetChatContext(Guid chatId, Guid userId)
    {
        var systemMessage = await GetSystemMessage(userId);
        var previousMessages = await GetPreviousMessages(chatId);
        return [
            systemMessage,
            ..previousMessages
        ];
    }

    private async Task<IList<GigaChatCompletionsRequestMessage>> GetPreviousMessages(Guid chatId)
    {
        var response = await chatsRepository.SearchChatMessages(new (chatId, 15, 0));
        return response.Items
            .Select(e => new GigaChatCompletionsRequestMessage()
            {
                Role = e.IsBot ? GigaChatCompletionsRequestMessageRole.Assistant : GigaChatCompletionsRequestMessageRole.User,
                Content = e.Message
            })
            .Reverse()
            .ToList();
    }

    private async Task<GigaChatCompletionsRequestMessage> GetSystemMessage(Guid userId)
    {
        var user = await usersRepository.Get(userId);
        var foodDiary = (await foodDiariesService.GetByDate(userId, DateOnly.FromDateTime(DateTime.Now))).Value;
        var message = string.Format(SystemMessageFormat,
            new UserContext(user.Weight, user.TargetWeight, user.Height, user.Gender),
            new NutrimentsContext(foodDiary.TotalNutriments, foodDiary.TotalEnergy,
                foodDiary.TargetNutriments, foodDiary.TargetEnergy));
        return new GigaChatCompletionsRequestMessage()
        {
            Role = GigaChatCompletionsRequestMessageRole.System,
            Content = message
        };
    }

    private record UserContext(
        float CurWeight,
        float TargetWeight,
        float Height,
        UserGender Gender)
    {
        public override string ToString()
        {
            return $@"
""Текущий вес"": {CurWeight},
""Целевой вес"": {TargetWeight},
""Рост"": {Height}, 
""пол"": {GenderToStr(Gender)}, 
";
        }

        private string GenderToStr(UserGender gender)
            => gender == UserGender.Male ? "мужчина" : "женщина";
    }

    private record NutrimentsContext(
        FoodNutriments CurrentNutriments,
        FoodEnergy CurrentEnergy,
        FoodNutriments TargetNutriments,
        FoodEnergy TargetEnergy)
    {
        public override string ToString()
        {
            return $@"
""калории, ккал"": {{
    ""потреблено"": {CurrentEnergy.Kcal},
    ""цель"": {TargetEnergy.Kcal},
}},
""белок, грамм"": {{
    ""потреблено"": {CurrentNutriments.Protein},
    ""цель"": {TargetNutriments.Protein},
}},
""жиры, грамм"": {{
    ""потреблено"": {CurrentNutriments.Fat}, 
    ""цель"": {TargetNutriments.Fat}
}},
""углеводы, грамм"": {{
    ""потреблено"": {CurrentNutriments.Carbohydrates},
    ""цель"": {TargetNutriments.Carbohydrates}
}}
";
        }
    }
    
    private const string SystemMessageFormat = @"
Ты - персональный ИИ-помощник по питанию и здоровью в приложении для подсчёта калорий. 
Твоё имя - Нутрибот. Ты всегда вежливый, поддерживающий и конкретный.

## ТВОЯ РОЛЬ:
1. Эксперт по нутрициологии - помогаешь сбалансировать питание
2. Личный мотивационный тренер - поддерживаешь и вдохновляешь
3. Практичный помощник - даёшь конкретные, выполнимые советы
4. Конфиденциальный ассистент - не разглашаешь данные пользователя

## КОНТЕКСТ ПОЛЬЗОВАТЕЛЯ:

// ✅ Ключевые данные 
{0}
 
// ✅ Текущие показатели дня 
{1},

## ПРАВИЛА ОБЩЕНИЯ:

1. НИКОГДА не спрашивай данные, которые уже есть в контексте - у тебя есть вся информация
2. Всегда персонализируй ответы - используй данные пользователя
3. Будь конкретным в рекомендациях - предлагай точные продукты и порции.
4. Рассчитывай остатки - учитывай оставшиеся квоты калорий и БЖУ
5. Давай советы, формируй привычки - фокусируйся на решениях, напоминай оважности здоровых привычек и их влиянии на процесс коррекции веса
6. Ты— не главный эксперт - Напоминай, что ты и приложение составляете только рекомендательную базу
7. Задавай вопросы - Не забывай уточнять подробности о пользователе, его привычках и распорядке дня.
6. Рекомендованный размеро ответов - 2-4 предложения на простые вопросы, 5-7 на сложные. Если пользователь запрашивает более объёмные ответы, то ориентируйся под запросы пользователя.

## ТОН И СТИЛЬ:
- Дружеский, но профессиональный
- Используй эмодзи умеренно (1-2 за ответ)
- Говори ""вы"" или по имени
- Будь позитивным, но реалистичным
";
}