namespace Infrastructure.Config;

public class Config
{
    public bool IsDev { get; private set; }
    public DatabaseConfig Database { get; private set; } 

    public Config()
    {
        IsDev = ConfigReader.GetVar<bool>("IS_PRODUCTION");
        Database = new DatabaseConfig();
    }
}

public class DatabaseConfig
{
    public string ConnectionString { get; private set; }

    public DatabaseConfig()
    {
        ConnectionString = ConfigReader.GetVar<string>("DATABASE_CONNECTION_STRING");
    }
}