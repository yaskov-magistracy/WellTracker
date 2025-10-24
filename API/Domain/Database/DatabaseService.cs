using Domain.Accounts.Admins;
using Domain.Accounts.Users;
using Domain.StaticFiles;
using Infrastructure;
using Infrastructure.Results;

namespace Domain.Database;

public interface IDatabaseService
{
    Task<EmptyResult> RecreateDatabase(bool withAutoFilling);
}

public class DatabaseService(
    IDatabaseAccessor databaseAccessor,
    IStaticFilesCleaner staticFilesCleaner,
    IAdminsService adminsService,
    IUsersService usersService
) : IDatabaseService
{
    public async Task<EmptyResult> RecreateDatabase(bool withAutoFilling)
    {
        try
        {
            await databaseAccessor.RecreateDatabase();
            // staticFilesCleaner.CleanUp(); TODO: fix `Device or resource busy : '/app/_staticFiles/'` 
            await adminsService.Register(new("admin", "admin"));
            await usersService.Register(new("user", "user", UserGender.Male, 91.32f, 179, UserTarget.LossWeight));
        }
        catch (Exception e)
        {
            return EmptyResults.BadRequest(e.Message);
        }

        return EmptyResults.NoContent();
    }
}