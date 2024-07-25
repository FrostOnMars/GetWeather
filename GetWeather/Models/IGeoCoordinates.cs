namespace GetWeather.Models;

public interface IGeoCoordinates
{
    GeoDatum[]? GeoData { get; set; }
}