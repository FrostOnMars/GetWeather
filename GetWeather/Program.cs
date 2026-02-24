using GetWeather.Controllers;
using GetWeather.Models;

List<CurrentWeather> WeatherData = [];
List<UrlController> UrlControllerList = [];

var cities = new List<string> { "London", "Paris", "Berlin", "New York", "Los Angeles" };
// Future extensibility: Could create an input for users to choose their own cities or read from a file

SelectedCities.Cities.Clear();

foreach (var city in cities)
{
    var urlController = new GeoDataUrlController(city);
    UrlControllerList.Add(urlController);

    var geoResult = OpenWeatherController.GetGeoCoordinates(city, urlController.FullUrl);

    if (!geoResult.Success)
    {
        // Diagnostic output for troubleshooting
        Console.WriteLine($"[GeoCoordinates FAILED] City: {city}");
        Console.WriteLine(geoResult.Error?.ToString());
        Console.WriteLine();

        // Continue with next city
        continue;
    }

    // If you want, you can also print how many matches were found:
    Console.WriteLine($"[GeoCoordinates OK] City: {city} | Matches: {geoResult.Data?.Count ?? 0}");
}

foreach (var selectedCity in SelectedCities.Cities)
{
    var locationParameterModel = selectedCity.Parameter;
    var urlController = new WeatherUrlController(locationParameterModel);
    UrlControllerList.Add(urlController);

    var weatherResult = OpenWeatherController.GetWeatherData(locationParameterModel, urlController.FullUrl);

    if (!weatherResult.Success)
    {
        Console.WriteLine($"[Weather FAILED] City: {selectedCity.City?.Name ?? "(unknown)"} " +
                          $"({locationParameterModel.Lat}, {locationParameterModel.Lon})");
        Console.WriteLine(weatherResult.Error?.ToString());
        Console.WriteLine();
        continue;
    }

    WeatherData.Add(weatherResult.Data!);

    Console.WriteLine($"[Weather OK] City: {selectedCity.City?.Name ?? "(unknown)"}");
}

// Only insert the weather records that were successfully retrieved
foreach (var weather in WeatherData)
{
    try
    {
        SqlController.InsertCurrentWeatherToDb(weather);
        Console.WriteLine("[DB OK] Inserted weather record.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("[DB FAILED] Could not insert weather record.");
        Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
        Console.WriteLine();
    }
}