using GetWeather.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using GetWeather.Utilities;

namespace GetWeather.Controllers;
public static class OpenWeatherController
{
    #region Public Methods

    public static GeoCoordinates GetGeoCoordinates(string city, string fullUrl, out LocationParameterModel locationParameterModel)
    {
        locationParameterModel = InitializeLocationParameterModel(city);
        var responseContent = ExecuteRequest(fullUrl);

        return DeserializeGeoCoordinates(responseContent, locationParameterModel);
    }

    public static CurrentWeather GetWeatherData(LocationParameterModel location, string fullUrl)
    {
        var responseContent = ExecuteRequest(fullUrl);

        return DeserializeWeatherData(responseContent);
    }

    #endregion Public Methods

    #region Private Methods

    private static LocationParameterModel InitializeLocationParameterModel(string city)
    {
        return new LocationParameterModel { City = city };
    }

    private static string ExecuteRequest(string url)
    {
        var client = new RestClient(url);
        var request = new RestRequest(url);
        request.AddHeader("Content-Type", "application/json");
        var response = client.Execute(request);

        return response.Content ?? string.Empty;
    }

    private static GeoCoordinates DeserializeGeoCoordinates(string responseContent, LocationParameterModel locationParameterModel)
    {
        var geoDatumType = typeof(GeoDatum);
        var geoDataType = geoDatumType.MakeArrayType();

        try
        {
            List<GeoDatum> geoCoordinatesList = [];
            if (JsonHelper.IsJsonArray(responseContent))
            {
                geoCoordinatesList = JsonConvert.DeserializeObject<List<GeoDatum>>(responseContent); 
            }
            else if (JsonHelper.IsJsonObject(responseContent))
            {
                geoCoordinatesList.Add(JsonConvert.DeserializeObject<GeoDatum>(responseContent));
            }
            else             
                throw new Exception("No data found");

            if (geoCoordinatesList != null && geoCoordinatesList.Count > 0)
            {
                return AssignGeoCoordinates(geoCoordinatesList, locationParameterModel);
            } 
            else
                throw new Exception("No data found");

            //var geoCoordinatesList = JsonConvert.DeserializeObject(responseContent, geoDataType) as List<GeoDatum>;
            //if (geoCoordinatesList != null && geoCoordinatesList.Count > 0)
            //{
            //    return AssignGeoCoordinates(geoCoordinatesList, locationParameterModel);
            //}

            //throw new Exception("No data found");
        }
        catch
        {
            //var geoCoordinatesSingle = JsonConvert.DeserializeObject(responseContent, geoDatumType) as GeoDatum;
            //if (geoCoordinatesSingle != null)
            //{
            //    return AssignGeoCoordinates(new List<GeoDatum> { geoCoordinatesSingle }, locationParameterModel);
            //}

            //throw new Exception("No data found");
            return new GeoCoordinates();
        }
    }

    private static GeoCoordinates AssignGeoCoordinates(List<GeoDatum> geoCoordinates, LocationParameterModel locationParameterModel)
    {
        var firstGeoDatum = geoCoordinates.First();

        locationParameterModel.Lat = firstGeoDatum.Lat.ToString(CultureInfo.CurrentCulture);
        locationParameterModel.Lon = firstGeoDatum.Lon.ToString(CultureInfo.CurrentCulture);
        locationParameterModel.Country = firstGeoDatum.Country ?? string.Empty;
        locationParameterModel.State = firstGeoDatum.State ?? string.Empty;

        return new GeoCoordinates
        {
            GeoData = geoCoordinates.ToArray()
        };
    }

    private static CurrentWeather DeserializeWeatherData(string responseContent)
    {
        var weather = JsonConvert.DeserializeObject<CurrentWeather>(responseContent);

        if (weather?.Weather == null || weather.Weather.Count == 0)
        {
            throw new Exception("No data found");
        }

        return weather;
    }

    #endregion Private Methods
}