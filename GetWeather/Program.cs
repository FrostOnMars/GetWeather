// See https://aka.ms/new-console-template for more information

using GetWeather.Controllers;
using Npgsql;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using GetWeather.Models;


var locationParameterModel = new LocationParameterModel();
var geoData = OpenWeatherController.GetGeoCoordinates("London", out locationParameterModel);
OpenWeatherController.GetWeatherData(locationParameterModel);


//1. fetch data, 2. model data, 3. store into SQL



public class AppConfig
{
    private AppConfig() { }
    //public static AppConfig Instance { get; } = new AppConfig();
    private static readonly Lazy<AppConfig> lazy = new Lazy<AppConfig>(() => new AppConfig());
    public static AppConfig Instance => lazy.Value;
    public string ApiKey { get; set; } = "e0322f2c8b62ca48ffa670e518c03a47";
    public string GeocodingUrl { get; set; } = "http://api.openweathermap.org/geo/1.0/direct";

    public string WeatherUrl { get; set; } = "https://api.openweathermap.org/data/2.5/weather";
    public string Lat { get; set; } = "33.35";
    public string Lon { get; set; } = "97.69";
    public string GeocodingUrlParameters => GetGeocodingUrlParameters();
    public string WeatherUrlParameters => GetWeatherUrlParameters();
    public string FullGeocodingUrl => $"{GeocodingUrl}?{GeocodingUrlParameters}";
    public string FullWeatherUrl => $"{WeatherUrl}?{WeatherUrlParameters}";
    public NpgsqlConnection Connection { get; set; } = new NpgsqlConnection("Host=localhost;Username=postgres;Password=password;Database=OpenWeather");

    public string GetGeocodingUrlParameters(string? city = null)
    {
        return city == null ? $"q=London,&limit=5&appid={ApiKey}": $"q={city},&limit=5&appid={ApiKey}";
    }
    public string GetWeatherUrlParameters()
    {
        return $"lat={Lat}&lon={Lon}&appid={ApiKey}";
    }
}