using Domain.Accounts.Admins;
using Domain.Accounts.Users;
using Domain.Database.DTO;
using Domain.Database.FoodsFilling;
using Domain.StaticFiles;
using Infrastructure;
using Infrastructure.Results;

namespace Domain.Database;

public interface IDatabaseService
{
    Task<EmptyResult> RecreateDatabase(RecreateDatabaseRequest request);
}

public class DatabaseService(
    IDatabaseAccessor databaseAccessor,
    IStaticFilesCleaner staticFilesCleaner,
    IAdminsService adminsService,
    IUsersService usersService,
    IDatabaseFoodsFiller databaseFoodsFiller
) : IDatabaseService
{
    public async Task<EmptyResult> RecreateDatabase(RecreateDatabaseRequest request)
    {
        try
        {
            await databaseAccessor.RecreateDatabase();
            // staticFilesCleaner.CleanUp(); TODO: fix `Device or resource busy : '/app/_staticFiles/'` 
            await adminsService.Register(new("admin", "admin"));
            await usersService.Register(new("user", "user", UserGender.Male, 91.32f, 179, UserTarget.LossWeight));
            if (request.AutoFillingParams == AutoFillingParams.SomeRealData)
            {
                await databaseFoodsFiller.FillFromRepo(100);
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
}