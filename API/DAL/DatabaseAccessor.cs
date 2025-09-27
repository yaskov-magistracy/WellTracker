using Domain.Database;

namespace DAL;

public class DatabaseAccessor(
    DataContext dataContext
    ) : IDatabaseAccessor
{
    public async Task RecreateDatabase()
    {
        await dataContext.Database.EnsureDeletedAsync();
        await dataContext.Database.EnsureCreatedAsync();
    }
}