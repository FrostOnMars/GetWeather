using GetWeather.Models;
using GetWeather.Models.FutureExtensibility;
using Npgsql;
using NpgsqlTypes;

namespace GetWeather.Controllers;

public class SqlController
{
    #region Private Fields

    private static readonly string _connectionString = AppConfig.ConnectionString;

    #endregion Private Fields

    #region Public Methods

    public static void InsertCurrentWeatherToDb(CurrentWeather weather)
    {
        using var connection = CreateConnection();
        try
        {
            //using var connection = _connectionString;
            using var command = CreateInsertWeatherCommand(weather, connection);
            connection.Open();

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    public List<LocationParameterModel> GetAllCities()
    {
        var cities = new List<LocationParameterModel>();

        using var connection = CreateConnection();
        //using var connection = _connectionString;
        using var command = CreateGetAllCitiesCommand(connection);
        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read()) cities.Add(MapReaderToLocationParameterModel(reader));

        return cities;
    }

    #endregion Public Methods

    #region Private Methods

    private static NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    private static DateTime ConvertToDateTime(int unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
    }

    private NpgsqlCommand CreateGetAllCitiesCommand(NpgsqlConnection connection)
    {
        return new NpgsqlCommand("SELECT * FROM GetAllCities()", connection);
    }

    private LocationParameterModel MapReaderToLocationParameterModel(NpgsqlDataReader reader)
    {
        return new LocationParameterModel
        {
            City = reader.GetString(reader.GetOrdinal("Name")),
            State = reader.IsDBNull(reader.GetOrdinal("State"))
                ? null
                : reader.GetString(reader.GetOrdinal("State")),
            Country = reader.GetString(reader.GetOrdinal("Country"))
        };
    }

    public static NpgsqlCommand CreateInsertWeatherCommand(CurrentWeather weather, NpgsqlConnection connection)
    {
        var command = new NpgsqlCommand(
            "CALL insert_weather_data(@city, @conditions, @weatherdate, @temperature, @feelslike, @clouds, @precipitation, @wind, @windspeed, @winddirection, @visibility)",
            connection);

        command.Parameters.AddWithValue("@city", NpgsqlDbType.Varchar, weather.Name);
        command.Parameters.AddWithValue("@conditions", NpgsqlDbType.Text, weather.Weather?.FirstOrDefault()?.Description ?? string.Empty);
        command.Parameters.AddWithValue("@weatherdate", NpgsqlDbType.Timestamp, ConvertToDateTime(weather.Dt));
        command.Parameters.AddWithValue("@temperature", NpgsqlDbType.Numeric, WeatherDescriber.GetCurrentTemperatureByTimeOfDay((float)weather.Main?.Temp).Temperature); //TODO: how to add null value?
        command.Parameters.AddWithValue("@feelslike", NpgsqlDbType.Varchar, WeatherDescriber.DescribeFeelsLike(weather.Main.FeelsLike));
        command.Parameters.AddWithValue("@clouds", NpgsqlDbType.Varchar, WeatherDescriber.ConvertCloudPercentToString(weather.Clouds));
        command.Parameters.AddWithValue("@precipitation", NpgsqlDbType.Varchar, WeatherDescriber.AddPrecipitationText(weather.Rain) ?? string.Empty);
        command.Parameters.AddWithValue("@wind", NpgsqlDbType.Varchar, WeatherDescriber.ConvertWindSpeedToBeaufortScale(weather.Wind));
        command.Parameters.AddWithValue("@windspeed", NpgsqlDbType.Numeric, weather.Wind?.Speed ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@winddirection", NpgsqlDbType.Varchar, WeatherDescriber.ConvertDegreesToDirection((double)weather.Wind?.Deg));
        command.Parameters.AddWithValue("@visibility", NpgsqlDbType.Integer, weather.Visibility);

        return command;
    }
    #endregion Private Methods
}