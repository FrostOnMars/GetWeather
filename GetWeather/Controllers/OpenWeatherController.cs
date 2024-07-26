using GetWeather.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;

namespace GetWeather.Controllers;

public static class OpenWeatherController
{
    #region Public Methods

    public static GeoCoordinates GetGeoCoordinates(string city, string fullUrl,
        out LocationParameterModel locationParameterModel)
    {
        //OpenClientRequest(); Question for Jon: can I use the same method to open both client requests using different urls?
        //GetGeoService();
        //AssignGeoCoordinates();

        locationParameterModel = new LocationParameterModel();
        locationParameterModel.City = city;
        var client = new RestClient(fullUrl);
        var request = new RestRequest(fullUrl);

        _ = request.AddHeader("Content-Type", "application/json");
        var response = client.Execute(request);

        var geoDatumType = typeof(GeoDatum);
        var geoDataType = geoDatumType.MakeArrayType();

        try
        {
            //Handles the case where the API returns an array of objects
            var geoCoordinates =
                JsonConvert.DeserializeObject(response.Content ?? string.Empty, geoDataType) as List<GeoDatum>;

            if (geoCoordinates is null || geoCoordinates.Count == 0) throw new Exception("No data found");

            locationParameterModel.Lat = geoCoordinates[0].Lat.ToString(CultureInfo.CurrentCulture);
            locationParameterModel.Lon = geoCoordinates[0].Lon.ToString(CultureInfo.CurrentCulture);
            locationParameterModel.Country = geoCoordinates[0].Country ?? string.Empty;
            locationParameterModel.State = geoCoordinates[0].State ?? string.Empty;

            return new GeoCoordinates
            {
                GeoData = geoCoordinates.ToArray()
            };
        }
        catch (Exception e)
        {
            //Handles the case where the API returns a single object instead of an array
            var geoCoordinates =
                JsonConvert.DeserializeObject(response.Content ?? string.Empty, geoDatumType) as GeoDatum;

            if (geoCoordinates is null) throw new Exception("No data found");

            locationParameterModel.Lat = geoCoordinates.Lat.ToString(CultureInfo.CurrentCulture);
            locationParameterModel.Lon = geoCoordinates.Lon.ToString(CultureInfo.CurrentCulture);
            locationParameterModel.Country = geoCoordinates.Country ?? string.Empty;
            locationParameterModel.State = geoCoordinates.State ?? string.Empty;

            return new GeoCoordinates
            {
                GeoData = new[] { geoCoordinates }
            };
        }
    }

    public static CurrentWeather GetWeatherData(LocationParameterModel location, string FullUrl)
    {
        //OpenClientRequest();
        //GetWeatherService();
        //AssignWeatherData();

        var client = new RestClient(FullUrl);
        var request = new RestRequest(FullUrl);

        _ = request.AddHeader("Content-Type", "application/json");
        var response = client.Execute(request);

        var weather = JsonConvert.DeserializeObject<CurrentWeather>(response.Content ?? string.Empty);

        if (weather?.Weather is null || weather.Weather?.Count == 0) throw new Exception("No data found");

        return weather;
    }

    #endregion Public Methods
}