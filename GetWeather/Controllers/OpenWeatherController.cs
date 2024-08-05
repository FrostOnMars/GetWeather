using GetWeather.Models;
using GetWeather.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using System.Runtime.InteropServices;

namespace GetWeather.Controllers;

public static class OpenWeatherController
{
    #region Public Methods

    public static void GetGeoCoordinates(string city, string fullUrl)
    {
        var responseContent = ExecuteRequest(fullUrl); //returns a list of city names and data
        DeserializeGeoCoordinates(responseContent);
    }

    public static CurrentWeather GetWeatherData(LocationParameterModel location, string fullUrl)
    {
        var responseContent = ExecuteRequest(fullUrl);

        return DeserializeWeatherData(responseContent);
    }

    #endregion Public Methods

    #region Private Methods

    private static void AssignGeoCoordinates(List<GeoDatum> geoCoordinates)
    {
        foreach (var selectedCity in geoCoordinates.Select(city => new SelectedCity
                 {
                     Parameter = new LocationParameterModel
                     {
                         Lat = city.Lat.ToString(CultureInfo.CurrentCulture),
                         Lon = city.Lon.ToString(CultureInfo.CurrentCulture),
                         Country = city.Country ?? string.Empty,
                         State = city.State ?? string.Empty
                     },
                     City = city
                 }))
        {
            SelectedCities.Cities.Add(selectedCity);
        }
    }

    private static void DeserializeGeoCoordinates(string responseContent)
    {
        try
        {
            List<GeoDatum> geoCoordinatesList = [];
            if (JsonHelper.IsJsonArray(responseContent))
                geoCoordinatesList = JsonConvert.DeserializeObject<List<GeoDatum>>(responseContent);
            else if (JsonHelper.IsJsonObject(responseContent))
                geoCoordinatesList.Add(JsonConvert.DeserializeObject<GeoDatum>(responseContent));
            else
                throw new Exception("No data found");

            if (geoCoordinatesList == null || geoCoordinatesList.Count <= 0) return;
            AssignGeoCoordinates(geoCoordinatesList);
        }
        catch
        {
            //TODO: error handling 
        }
    }

    private static CurrentWeather DeserializeWeatherData(string responseContent)
    {
        var weather = JsonConvert.DeserializeObject<CurrentWeather>(responseContent);

        if (weather?.Weather == null || weather.Weather.Count == 0) throw new Exception("No data found");

        return weather;
    }

    private static string ExecuteRequest(string url)
    {
        var client = new RestClient(url);
        var request = new RestRequest(url);
        request.AddHeader("Content-Type", "application/json");
        var response = client.Execute(request);

        return response.Content ?? string.Empty;
    }

    #endregion Private Methods
}