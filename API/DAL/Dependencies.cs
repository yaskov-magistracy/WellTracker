#nullable enable

using DAL.Accounts;
using DAL.Accounts.Admins;
using DAL.Accounts.Users;
using DAL.Chats;
using DAL.ExerciseDiaries;
using DAL.Exercises;
using DAL.FoodDiaries;
using DAL.Foods;
using DAL.StaticFiles;
using DAL.Statistics.Weight;
using Domain.Accounts;
using Domain.Accounts.Admins;
using Domain.Accounts.Users;
using Domain.Advices;
using Domain.Chats;
using Domain.Database;
using Domain.Database.FoodsFilling;
using Domain.Database.Github;
using Domain.Database.Helpers;
using Domain.ExerciseDiaries;
using Domain.Exercises;
using Domain.FoodDiaries;
using Domain.Foods;
using Domain.StaticFiles;
using Domain.Statistics.Calories;
using Domain.Statistics.Weight;
using GigaChat;
using Infrastructure.Config;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace DAL;

public class Dependencies
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<Config>();
        services.AddDbContext<DataContext>();
        
        services.AddSingleton<TelegramBotClient>(s 
            => new TelegramBotClient(s.GetRequiredService<Config>().Telegram.Token));

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IAdminsService, AdminsService>();
        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        
        services.AddSingleton<IGigaChatClient, GigaChatClient>(s =>
        {
            var config = (s.GetRequiredService<Config>()).GigaChat;
            return new GigaChatClient(config.AuthorizationKey, config.Scope);
        });
        services.AddScoped<IChatsService, ChatsService>();
        services.AddScoped<IChatsRepository, ChatsRepository>();
        
        services.AddSingleton<IGitHubFileReader, GitHubFileReader>();
        services.AddScoped<IDatabaseFoodsFiller, DatabaseFoodsFiller>();
        services.AddScoped<IDatabaseService, DatabaseService>();
        services.AddScoped<IDatabaseExercisesFiller, DatabaseExercisesFiller>();
        services.AddScoped<IDatabaseAccessor, DatabaseAccessor>();

        services.AddScoped<IExerciseDiariesService, ExerciseDiaryService>();
        services.AddScoped<IExerciseDiariesRepository, ExerciseDiariesRepository>();

        services.AddScoped<IExercisesService, ExercisesService>();
        services.AddScoped<IExercisesRepository, ExercisesRepository>();
        
        services.AddScoped<IFoodDiariesService, FoodDiariesService>();
        services.AddScoped<IFoodDiariesRepository, FoodDiariesRepository>();

        services.AddScoped<IFoodsService, FoodsService>();
        services.AddScoped<IFoodsRepository, FoodsRepository>();
        
        services.AddScoped<IStaticFilesService, StaticFilesService>();
        services.AddSingleton<IStaticFilesProvider, LocalFilesProvider>();
        services.AddSingleton<IStaticFilesCleaner, LocalFilesProvider>();
        services.AddScoped<IStaticFilesRepository, StaticFilesRepository>();
        
        services.AddScoped<IWeightRecordsService, WeightRecordsService>();
        services.AddScoped<INutrimentsStatisticsService, NutrimentsStatisticsService>();
        services.AddScoped<IAdvicesService, AdvicesService>();
        services.AddScoped<IWeightRecordsRepository, WeightRecordsRepository>();
        
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddScoped<IReportsGigaChatRequestGenerator, ReportsGigaChatRequestGenerator>();
        services.AddScoped<IReportsService, ReportsService>();
    }
}