﻿using Npgsql;

public class AppConfig
{
    #region Private Fields

    //public static AppConfig Instance { get; } = new AppConfig();

    #endregion Private Fields

    #region Private Constructors

    private AppConfig()
    {
    }

    #endregion Private Constructors

    #region Public Properties

    public static AppConfig Instance => Lazy.Value;
    public static Lazy<AppConfig> Lazy { get; } = new(() => new AppConfig());

    public string ApiKey { get; set; } = "e0322f2c8b62ca48ffa670e518c03a47";

    public NpgsqlConnection Connection { get; set; } =
        new("Host=localhost;Username=postgres;Password=password;Database=OpenWeather");

    #endregion Public Properties

    //TODO: implement sql connection
}