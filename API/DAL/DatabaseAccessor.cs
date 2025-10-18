using Domain.Database;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class DatabaseAccessor(
    DataContext dataContext
    ) : IDatabaseAccessor
{
    public async Task RecreateDatabase()
    {
        await dataContext.Database.EnsureDeletedAsync();
        await dataContext.Database.EnsureCreatedAsync();

        await EnsureRussianConfigInTsVectors();
    }
    
    private async Task EnsureRussianConfigInTsVectors()
    {
        // TODO
        // var configExists1 = dataContext.Database
        //     .SqlQueryRaw<object>("SELECT * FROM pg_ts_config");
        // var configExists = await dataContext.Database
        //         .ExecuteScalarRawAsync<bool>(
        //             "SELECT EXISTS (SELECT 1 FROM pg_ts_config WHERE cfgname = 'russian')");
        //     .FirstOrDefaultAsync();
        // if (!configExists)
        //     throw new Exception("There is no RussianConfig for PgSQL TsVector");
    }
}