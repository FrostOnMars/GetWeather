using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetWeather.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GetWeather.Controllers;

public static class OpenWeatherController
{
    //call the class and get its data
    public static GeoCoordinates GetGeoCoordinates(string city, out LocationParameterModel locationParameterModel)
    {
        AppConfig.Instance.City = city;
        locationParameterModel = new LocationParameterModel();
        locationParameterModel.City = city;
        var client = new RestClient(AppConfig.Instance.FullGeocodingUrl);
        var request = new RestRequest(AppConfig.Instance.FullGeocodingUrl, Method.Get);

        _ = request.AddHeader("Content-Type", "application/json");
        var response = client.Execute(request);
        
        //if (response.IsSuccessful)
        //{
        //    Console.WriteLine(response.Content);
        //}
        //else
        //{
        //    Console.WriteLine("Error: " + response.ErrorMessage);
        //}
        var geoCoordinates = JsonConvert.DeserializeObject<List<GeoDatum>>(response.Content ?? string.Empty);
        
        if (geoCoordinates is null || geoCoordinates.Count == 0)
        {
            throw new Exception("No data found");
        }

        locationParameterModel.Lat = geoCoordinates[0].Lat.ToString(CultureInfo.CurrentCulture);
        locationParameterModel.Lon = geoCoordinates[0].Lon.ToString(CultureInfo.CurrentCulture);
        locationParameterModel.Country = geoCoordinates[0].Country ?? string.Empty;
        locationParameterModel.State = geoCoordinates[0].State ?? string.Empty;
        
        return new GeoCoordinates
        {
            GeoData = geoCoordinates.ToArray()
        };
    }

    public static CurrentWeather GetWeatherData(LocationParameterModel location)
    {
        AppConfig.Instance.LocationParameter = location;
        var client = new RestClient(AppConfig.Instance.FullWeatherUrl);
        var request = new RestRequest(AppConfig.Instance.FullWeatherUrl, Method.Get);

        _ = request.AddHeader("Content-Type", "application/json");
        var response = client.Execute(request);

        var weather = JsonConvert.DeserializeObject<CurrentWeather>(response.Content ?? string.Empty);

        if (weather?.weather is null || weather.weather?.Count == 0)
        {
            throw new Exception("No data found");
        }
        
        return weather;
    }
}