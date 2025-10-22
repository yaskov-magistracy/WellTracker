﻿namespace Infrastructure.Config;

public class Config
{
    public bool IsDev { get; private set; } = ConfigReader.GetVar<string>("APP_ENVIRONMENT") != "PRODUCTION";
    public DatabaseConfig Database { get; private set; } = new();
    public TelegramBotConfig Telegram { get; private set; } = new();
}

public class DatabaseConfig
{
    public string ConnectionString { get; private set; }

    public DatabaseConfig()
    {
        ConnectionString = ConfigReader.GetVar<string>("DATABASE_CONNECTION_STRING");
    }
}

public class TelegramBotConfig
{
    public string Token { get; private set; }

    public TelegramBotConfig()
    {
        Token = ConfigReader.GetVar<string>("TELEGRAM_BOT_TOKEN");
    }
}