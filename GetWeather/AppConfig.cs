using GetWeather.Models;
using Newtonsoft.Json.Linq;
using Npgsql;

public class AppConfig
{
    //TODO: Refactor this AppConfig. 1. SOLID: should only do 1 thing. Our properties violate this.
    //Question: is my program using the Lat/Lon from Appconfig or from OpenWeatherController?
    public static Lazy<AppConfig> Lazy => lazy;

    private AppConfig() { }
    //public static AppConfig Instance { get; } = new AppConfig();
    private static readonly Lazy<AppConfig> lazy = new Lazy<AppConfig>(() => new AppConfig());
    public static AppConfig Instance => lazy.Value;
    public string ApiKey { get; set; } = "e0322f2c8b62ca48ffa670e518c03a47";
    public NpgsqlConnection Connection { get; set; } = new NpgsqlConnection("Host=localhost;Username=postgres;Password=password;Database=OpenWeather");

    
}