using Infrastructure;
using Infrastructure.Results;

namespace Domain.Database;

public interface IDatabaseService
{
    Task<EmptyResult> RecreateDatabase();
}

public class DatabaseService(
    IDatabaseAccessor databaseAccessor
) : IDatabaseService
{
    public async Task<EmptyResult> RecreateDatabase()
    {
        try
        {
            await databaseAccessor.RecreateDatabase();
        }
        catch (Exception e)
        {
            return EmptyResults.BadRequest(e.Message);
        }

        return EmptyResults.NoContent();
    }
}