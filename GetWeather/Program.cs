// See https://aka.ms/new-console-template for more information

using GetWeather.Controllers;
using Npgsql;
using System.Collections.Generic;
using System.Diagnostics.Metrics;


var test = AppConfig.Instance;
var openWeatherController = new OpenWeatherController();

openWeatherController.GetWeather();

//1. fetch data, 2. model data, 3. store into SQL



public class AppConfig
{
    private AppConfig() { }
    //public static AppConfig Instance { get; } = new AppConfig();
    private static readonly Lazy<AppConfig> lazy = new Lazy<AppConfig>(() => new AppConfig());
    public static AppConfig Instance => lazy.Value;
    public string ApiKey { get; set; } = "e0322f2c8b62ca48ffa670e518c03a47";
    //public string Url { get; set; } = "http://api.openweathermap.org/data/2.5/forecast";
    public string Url { get; set; } = "http://api.openweathermap.org/geo/1.0/direct";
    public string Lat { get; set; } = "33.35";
    public string Lon { get; set; } = "97.69";
    public string UrlParameters => GetUrlParameters();
    public string FullUrl => $"{Url}?{UrlParameters}";
    public NpgsqlConnection Connection { get; set; } = new NpgsqlConnection("Host=localhost;Username=postgres;Password=password;Database=OpenWeather");

    public string GetUrlParameters()
    {
        //return $"lat={Lat}&lon={Lon}&appid={ApiKey}";
        //return $"?id=524901&appid={ApiKey}";
        //return $"http://api.openweathermap.org/geo/1.0/direct?q={city name},{state code},{country code}&limit={limit}&appid={ApiKey}"
        return $"q=London,&limit=5&appid={ApiKey}";
    }
}