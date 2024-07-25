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
    public static GeoCoordinates GetGeoCoordinates(string city, string fullUrl, out LocationParameterModel locationParameterModel)
    {
        //OpenClientRequest(); Question for Jon: can I use the same method to open both client requests using different urls?
        //GetGeoService();
        //AssignGeoCoordinates();
        
        locationParameterModel = new LocationParameterModel();
        locationParameterModel.City = city;
        var client = new RestClient(fullUrl);
        var request = new RestRequest(fullUrl, Method.Get);

        _ = request.AddHeader("Content-Type", "application/json");
        var response = client.Execute(request);

        Type geoDatumType = typeof(GeoDatum);
        Type geoDataType = geoDatumType.MakeArrayType();

        try
        {
            //Handles the case where the API returns an array of objects
            var geoCoordinates = JsonConvert.DeserializeObject(response.Content ?? string.Empty, geoDataType) as List<GeoDatum>;

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
        catch (Exception e)
        {
            //Handles the case where the API returns a single object instead of an array
            var geoCoordinates = JsonConvert.DeserializeObject(response.Content ?? string.Empty, geoDatumType) as GeoDatum;

            if (geoCoordinates is null)
            {
                throw new Exception("No data found");
            }

            locationParameterModel.Lat = geoCoordinates.Lat.ToString(CultureInfo.CurrentCulture);
            locationParameterModel.Lon = geoCoordinates.Lon.ToString(CultureInfo.CurrentCulture);
            locationParameterModel.Country = geoCoordinates.Country ?? string.Empty;
            locationParameterModel.State = geoCoordinates.State ?? string.Empty;

            return new GeoCoordinates
            {
                GeoData = new GeoDatum[] { geoCoordinates }

            };
        }
    }

    public static CurrentWeather GetWeatherData(LocationParameterModel location, string FullUrl)
    {
        //OpenClientRequest();
        //GetWeatherService();
        //AssignWeatherData();

        var client = new RestClient(FullUrl);
        var request = new RestRequest(FullUrl, Method.Get);

        _ = request.AddHeader("Content-Type", "application/json");
        var response = client.Execute(request);

        var weather = JsonConvert.DeserializeObject<CurrentWeather>(response.Content ?? string.Empty);

        if (weather?.Weather is null || weather.Weather?.Count == 0)
        {
            throw new Exception("No data found");
        }
        
        return weather;
    }
}