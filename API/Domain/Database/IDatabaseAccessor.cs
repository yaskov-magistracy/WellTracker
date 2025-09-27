namespace Domain.Database;

public interface IDatabaseAccessor
{
    Task RecreateDatabase();
}