// See https://aka.ms/new-console-template for more information

using GetWeather.Controllers;
using GetWeather.Models;

List<CurrentWeather> WeatherData = [];
List<UrlController> UrlControllerList = [];

var cities = new List<string> { "London", "Paris", "Berlin", "New York", "Los Angeles" };
//Future extensibility: Could create an input for users to choose their own cities or read from a file

foreach (var city in cities)
{
    var urlController = new GeoDataUrlController(city);
    UrlControllerList.Add(urlController);
    OpenWeatherController.GetGeoCoordinates(city, urlController.FullUrl);
    
}

foreach (var selectedCity in SelectedCities.Cities)
{
    var locationParameterModel = selectedCity.Parameter;
    var urlController = new WeatherUrlController(locationParameterModel);
    UrlControllerList.Add(urlController);
    var weatherData = OpenWeatherController.GetWeatherData(locationParameterModel, urlController.FullUrl);
    WeatherData.Add(weatherData);
}
foreach (var weather in WeatherData) SqlController.InsertCurrentWeatherToDb(weather);