using GetWeather.Models;
using Npgsql;

namespace GetWeather.Controllers;

public class SqlController
{
    #region Private Fields

    private readonly string _connectionString;

    #endregion Private Fields

    #region Constructors

    public SqlController(string connectionString)
    {
        _connectionString = connectionString;
    }

    #endregion Constructors

    #region Public Methods

    public void InsertCurrentWeatherToDb(CurrentWeather weather)
    {
        using (var connection = CreateConnection())
        using (var command = CreateInsertWeatherCommand(weather, connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public List<LocationParameterModel> GetAllCities()
    {
        var cities = new List<LocationParameterModel>();

        using (var connection = CreateConnection())
        using (var command = CreateGetAllCitiesCommand(connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cities.Add(MapReaderToLocationParameterModel(reader));
                }
            }
        }

        return cities;
    }

    #endregion Public Methods

    #region Private Methods

    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    private NpgsqlCommand CreateInsertWeatherCommand(CurrentWeather weather, NpgsqlConnection connection)
    {
        var command = new NpgsqlCommand(
            "CALL InsertCurrentWeatherToDb(@City, @Conditions, @WeatherDate, @Temperature, @Clouds, @Precipitation, @Rain, @Snow, @Windspeed, @WindDirection, @Visibility)",
            connection);

        command.Parameters.AddWithValue("City", weather.Name);
        command.Parameters.AddWithValue("Conditions", weather.Weather.First().Description ?? string.Empty);
        //command.Parameters.AddWithValue("WeatherDate", ConvertToDateTime(weather.Dt));
        command.Parameters.AddWithValue("Temperature", weather.Main.Temp);
        command.Parameters.AddWithValue("Clouds", weather.Clouds);
        command.Parameters.AddWithValue("Precipitation", weather.Rain.OneHour);
        command.Parameters.AddWithValue("Rain", weather.Rain);
        //command.Parameters.AddWithValue("Snow", weather.Snow ?? DBNull.Value);
        command.Parameters.AddWithValue("Windspeed", weather.Wind.Speed);
        command.Parameters.AddWithValue("WindDirection", weather.Wind.Deg);
        command.Parameters.AddWithValue("Visibility", weather.Visibility);

        return command;
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
            State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
            Country = reader.GetString(reader.GetOrdinal("Country"))
        };
    }

    private DateTime ConvertToDateTime(int unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
    }

    #endregion Private Methods
}