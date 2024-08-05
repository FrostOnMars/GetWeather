using GetWeather.Models;
using Newtonsoft.Json;
using Npgsql;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeather.Controllers;

//This class stores the old version of OpenWeatherController before refactoring.
//Kept here for reference in case I broke something.


//public static class TemporaryClass
//{
//    #region Public Methods

//    public static GeoCoordinates GetGeoCoordinates(string city, string fullUrl,
//        out LocationParameterModel locationParameterModel)
//    {
//        locationParameterModel = new LocationParameterModel();
//        locationParameterModel.City = city;

//        var client = new RestClient(fullUrl);
//        var request = new RestRequest(fullUrl);

//        _ = request.AddHeader("Content-Type", "application/json");
//        var response = client.Execute(request);

//        var geoDatumType = typeof(GeoDatum);
//        var geoDataType = geoDatumType.MakeArrayType();

//        try
//        {
//            //Handles the case where the API returns an array of objects
//            var geoCoordinates =
//                JsonConvert.DeserializeObject(response.Content ?? string.Empty, geoDataType) as List<GeoDatum>;

//            if (geoCoordinates is null || geoCoordinates.Count == 0) throw new Exception("No data found");

//            locationParameterModel.Lat = geoCoordinates[0].Lat.ToString(CultureInfo.CurrentCulture);
//            locationParameterModel.Lon = geoCoordinates[0].Lon.ToString(CultureInfo.CurrentCulture);
//            locationParameterModel.Country = geoCoordinates[0].Country ?? string.Empty;
//            locationParameterModel.State = geoCoordinates[0].State ?? string.Empty;

//            return new GeoCoordinates
//            {
//                GeoData = geoCoordinates.ToArray()
//            };
//        }
//        catch (Exception e)
//        {
//            //Handles the case where the API returns a single object instead of an array
//            var geoCoordinates =
//                JsonConvert.DeserializeObject(response.Content ?? string.Empty, geoDatumType) as GeoDatum;

//            if (geoCoordinates is null) throw new Exception("No data found");

//            locationParameterModel.Lat = geoCoordinates.Lat.ToString(CultureInfo.CurrentCulture);
//            locationParameterModel.Lon = geoCoordinates.Lon.ToString(CultureInfo.CurrentCulture);
//            locationParameterModel.Country = geoCoordinates.Country ?? string.Empty;
//            locationParameterModel.State = geoCoordinates.State ?? string.Empty;

//            return new GeoCoordinates
//            {
//                GeoData = new[] { geoCoordinates }
//            };
//        }
//    }

//    public static CurrentWeather GetWeatherData(LocationParameterModel location, string FullUrl)
//    {
//        //OpenClientRequest();
//        //GetWeatherService();
//        //AssignWeatherData();

//        var client = new RestClient(FullUrl);
//        var request = new RestRequest(FullUrl);

//        _ = request.AddHeader("Content-Type", "application/json");
//        var response = client.Execute(request);

//        var weather = JsonConvert.DeserializeObject<CurrentWeather>(response.Content ?? string.Empty);

//        if (weather?.Weather is null || weather.Weather?.Count == 0) throw new Exception("No data found");

//        return weather;
//    }

//    #endregion Public Methods
//}


//DEMARCATION LINE

//The following is the old version of SqlController.cs before refactoring.

//public class SqlController
//{
//    //TODO: Implement SQL connection, create and organize tables, and write SQL queries to store and retrieve weather data.

//    #region Private Fields

//    private string _connectionString =
//        "Host=PostgreSQL 16;Port=5432;Database=OpenWeather;User Id=postgres; Password=password;";

//    #endregion Private Fields


//    public void InsertCurrentWeather(CurrentWeather weather)
//    {
//        using (var connection = new NpgsqlConnection(_connectionString))
//        {
//            using (var command =
//                   new NpgsqlCommand(
//                       "CALL InsertCurrentWeather(@City, @Conditions, @WeatherDate, @Temperature, @Clouds, @Precipitation, @Rain, @Snow, @Windspeed, @WindDirection, @Visibility)",
//                       connection))
//            {
//                command.Parameters.AddWithValue("City", weather.Name);
//                command.Parameters.AddWithValue("Conditions", weather.Weather.First().Description ?? string.Empty);
//                //command.Parameters.AddWithValue("WeatherDate", weather.Dt ?? DBNull.Value); //Look up how to translate from int date to DateTime date
//                command.Parameters.AddWithValue("Temperature", weather.Main.Temp);
//                command.Parameters.AddWithValue("Clouds", weather.Clouds);
//                command.Parameters.AddWithValue("Precipitation", weather.Rain.OneHour);
//                command.Parameters.AddWithValue("Rain", weather.Rain);
//                //command.Parameters.AddWithValue("Snow", weather. ?? DBNull.Value);
//                command.Parameters.AddWithValue("Windspeed", weather.Wind.Speed);
//                command.Parameters.AddWithValue("WindDirection", weather.Wind.Deg);
//                command.Parameters.AddWithValue("Visibility", weather.Visibility);

//                connection.Open();
//                command.ExecuteNonQuery();
//            }
//        }
//    }

//    public List<LocationParameterModel> GetAllCities()
//    {
//        var cities = new List<LocationParameterModel>();

//        using (var connection = new NpgsqlConnection(_connectionString))
//        {
//            connection.Open();
//            using (var command = new NpgsqlCommand("SELECT * FROM GetAllCities()", connection))
//            {
//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        var city = new LocationParameterModel()
//                        {
//                            City = reader.GetString(reader.GetOrdinal("Name")),
//                            State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
//                            Country = reader.GetString(reader.GetOrdinal("Country"))
//                        };
//                        cities.Add(city);
//                    }
//                }
//            }
//        }

//        return cities;
//    }

//}