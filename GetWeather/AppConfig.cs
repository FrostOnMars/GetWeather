public class AppConfig
{
    #region Private Constructors

    private AppConfig()
    {
    }

    #endregion Private Constructors

    #region Private Fields

    //public static AppConfig Instance { get; } = new AppConfig();

    #endregion Private Fields

    #region Public Properties

    public static string ConnectionString { get; set; } =
        new("Host=localhost;Username=postgres;Password=password;Database=OpenWeather");

    public static AppConfig Instance => Lazy.Value;
    public static Lazy<AppConfig> Lazy { get; } = new(() => new AppConfig());

    public string ApiKey { get; set; } = "e0322f2c8b62ca48ffa670e518c03a47";

    #endregion Public Properties

    //TODO: implement sql connection
}