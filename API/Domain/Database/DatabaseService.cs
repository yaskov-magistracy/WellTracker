using Domain.Accounts.Admins;
using Domain.Accounts.Users;
using Domain.Database.DTO;
using Domain.Database.FoodsFilling;
using Domain.Database.Helpers;
using Domain.StaticFiles;
using Domain.Statistics.Weight;
using Infrastructure;
using Infrastructure.Results;

namespace Domain.Database;

public interface IDatabaseService
{
    Task<EmptyResult> RecreateDatabase(RecreateDatabaseRequest request);
    
    Task FillFromRepo(
        int? maxEntities = null,
        int maxInChunk = 20);
}

public class DatabaseService(
    IDatabaseAccessor databaseAccessor,
    IStaticFilesCleaner staticFilesCleaner,
    IAdminsService adminsService,
    IUsersService usersService,
    IDatabaseFoodsFiller databaseFoodsFiller,
    IDatabaseExercisesFiller databaseExercisesFiller,
    IWeightRecordsService weightRecordsService
) : IDatabaseService
{
    public async Task<EmptyResult> RecreateDatabase(RecreateDatabaseRequest request)
    {
        try
        {
            await databaseAccessor.RecreateDatabase();
            // staticFilesCleaner.CleanUp(); TODO: fix `Device or resource busy : '/app/_staticFiles/'` 
            await adminsService.Register(new("admin", "admin"));
            var user = await usersService.Register(
                new("user", "user", UserGender.Male, 91.32f, 179, 80.23f));
            var prevDay = DateTime.Now.AddDays(-1);
            await weightRecordsService
                .AddOrUpdate(user.Value.Id, user.Value.Weight - 5, DateOnly.FromDateTime(prevDay));
            await weightRecordsService
                .AddOrUpdate(user.Value.Id, user.Value.Weight - 5, DateOnly.FromDateTime(prevDay.AddDays(-1)));
            if (request.AutoFillingParams == AutoFillingParams.SomeRealData)
            {
                await databaseFoodsFiller.FillFromRepo(100);
                await databaseExercisesFiller.FillFromRepo(100);
            }
            if (request.AutoFillingParams == AutoFillingParams.FullRealData)
            {
                throw new NotImplementedException("Полное заполнение ещё не поддерживается");
            }
        }
        catch (Exception e)
        {
            return EmptyResults.BadRequest(e.Message);
        }

        return EmptyResults.NoContent();
    }

    public Task FillFromRepo(int? maxEntities = null, int maxInChunk = 20)
        => databaseFoodsFiller.FillFromRepo();
}