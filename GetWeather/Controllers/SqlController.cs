using GetWeather.Models;
using Npgsql;
using NpgsqlTypes;

namespace GetWeather.Controllers;

public class SqlController
{

    private static readonly NpgsqlConnection _connectionString = AppConfig.ConnectionString;


    #region Public Methods

    public static void InsertCurrentWeatherToDb(CurrentWeather weather)
    {
        //using var connection = CreateConnection();
        using var connection = _connectionString;
        connection.Open();
        using var command = CreateInsertWeatherCommand(weather, connection);
        
        command.ExecuteNonQuery();
    }

    public List<LocationParameterModel> GetAllCities()
    {
        var cities = new List<LocationParameterModel>();

        //using var connection = CreateConnection();
        using var connection = _connectionString;
        using var command = CreateGetAllCitiesCommand(connection);
        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            cities.Add(MapReaderToLocationParameterModel(reader));
        }

        return cities;
    }

    #endregion Public Methods

    #region Private Methods

    //private static NpgsqlConnection CreateConnection()
    //{
    //    return new NpgsqlConnection(_connectionString);
    //}

    public static NpgsqlCommand CreateInsertWeatherCommand(CurrentWeather weather, NpgsqlConnection connection)
    {
        //var command = new NpgsqlCommand(
        //    "CALL InsertCurrentWeatherToDb(@City, @Conditions, @WeatherDate, @Temperature, @Clouds, @Precipitation, @Rain, @Snow, @Windspeed, @WindDirection, @Visibility)",
        //    connection);
        {
            if (weather?.Main == null)
            {
                throw new ArgumentNullException(nameof(weather), "Weather or its Main property cannot be null");
            }

            var command = new NpgsqlCommand("INSERT INTO Weather (City, Temperature) VALUES (@City, @Temperature)",
                connection);

            command.Parameters.Add(new NpgsqlParameter("@City", NpgsqlDbType.Varchar) { Value = weather.Name });
            command.Parameters.Add(new NpgsqlParameter("@Temperature", NpgsqlDbType.Double)
                { Value = weather.Main.Temp });
            return command;

            // command.Parameters.Add("City", weather.Name);
            // command.Parameters.AddWithValue("Conditions", weather.Weather?.FirstOrDefault()?.Description ?? string.Empty);
            // command.Parameters.AddWithValue("WeatherDate", ConvertToDateTime(weather.Dt));
            // command.Parameters.AddWithValue("Temperature", weather.Main?.Temp);
            // command.Parameters.AddWithValue("Clouds", weather.Clouds);
            // command.Parameters.AddWithValue("Precipitation", weather.Rain?.OneHour ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("Rain", weather.Rain);
            //// command.Parameters.AddWithValue("Snow", weather.Snow ?? DBNull.Value);
            // command.Parameters.AddWithValue("Windspeed", weather.Wind?.Speed ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("WindDirection", weather.Wind?.Deg ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("Visibility", weather.Visibility);

            // return command;
        }

        #endregion Private Methods

    }
    NpgsqlCommand CreateGetAllCitiesCommand(NpgsqlConnection connection)
    {
        return new NpgsqlCommand("SELECT * FROM GetAllCities()", connection);
    }

    LocationParameterModel MapReaderToLocationParameterModel(NpgsqlDataReader reader)
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

    DateTime ConvertToDateTime(int unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
    }
}