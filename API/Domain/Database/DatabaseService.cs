using Domain.Accounts.Admins;
using Infrastructure;
using Infrastructure.Results;

namespace Domain.Database;

public interface IDatabaseService
{
    Task<EmptyResult> RecreateDatabase(bool withAutoFilling);
}

public class DatabaseService(
    IDatabaseAccessor databaseAccessor,
    IAdminsService adminsService
) : IDatabaseService
{
    public async Task<EmptyResult> RecreateDatabase(bool withAutoFilling)
    {
        try
        {
            await databaseAccessor.RecreateDatabase();
            await adminsService.Register(new("admin", "admin"));
        }
        catch (Exception e)
        {
            return EmptyResults.BadRequest(e.Message);
        }

        return EmptyResults.NoContent();
    }
}