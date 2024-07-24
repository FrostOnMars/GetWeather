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
        var geoCoordinates = JsonConvert.DeserializeObject<GeoCoordinates>(response.Content ?? string.Empty);
        
        if (geoCoordinates?.GeoData is null || geoCoordinates.GeoData?.Length == 0)
        {
            throw new Exception("No data found");
        }

        locationParameterModel.Lat = geoCoordinates.GeoData[0].Lat.ToString(CultureInfo.CurrentCulture);
        locationParameterModel.Lon = geoCoordinates.GeoData[0].Lon.ToString(CultureInfo.CurrentCulture);
        locationParameterModel.Country = geoCoordinates.GeoData[0].Country ?? string.Empty;
        locationParameterModel.State = geoCoordinates.GeoData[0].State ?? string.Empty;
        
        return geoCoordinates;
    }

    public static void GetWeatherData(LocationParameterModel location)
    {

    }
}