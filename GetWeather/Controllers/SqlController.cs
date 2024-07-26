using GetWeather.Models;
using Npgsql;

namespace GetWeather.Controllers;

public class SqlController
{
    //TODO: Implement SQL connection, create and organize tables, and write SQL queries to store and retrieve weather data.
    
    #region Private Fields

    private string _connectionString =
        "Host=PostgreSQL 16;Port=5432;Database=OpenWeather;User Id=postgres; Password=password;";

    #endregion Private Fields


    public void InsertCurrentWeather(CurrentWeather weather)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            using (var command =
                   new NpgsqlCommand(
                       "CALL InsertCurrentWeather(@City, @Conditions, @WeatherDate, @Temperature, @Clouds, @Precipitation, @Rain, @Snow, @Windspeed, @WindDirection, @Visibility)",
                       connection))
            {
                command.Parameters.AddWithValue("City", weather.Name);
                command.Parameters.AddWithValue("Conditions", weather.Weather.First().Description ?? string.Empty);
                //command.Parameters.AddWithValue("WeatherDate", weather.Dt ?? DBNull.Value); //Look up how to translate from int date to DateTime date
                command.Parameters.AddWithValue("Temperature", (object)temperature ?? DBNull.Value);
                command.Parameters.AddWithValue("Clouds", (object)clouds ?? DBNull.Value);
                command.Parameters.AddWithValue("Precipitation", (object)precipitation ?? DBNull.Value);
                command.Parameters.AddWithValue("Rain", (object)rain ?? DBNull.Value);
                command.Parameters.AddWithValue("Snow", (object)snow ?? DBNull.Value);
                command.Parameters.AddWithValue("Windspeed", (object)windspeed ?? DBNull.Value);
                command.Parameters.AddWithValue("WindDirection", (object)windDirection ?? DBNull.Value);
                command.Parameters.AddWithValue("Visibility", (object)visibility ?? DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public List<LocationParameterModel> GetAllCities()
    {
        var cities = new List<LocationParameterModel>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM GetAllCities()", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var city = new LocationParameterModel()
                        {
                            City = reader.GetString(reader.GetOrdinal("Name")),
                            State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
                            Country = reader.GetString(reader.GetOrdinal("Country"))
                        };
                        cities.Add(city);
                    }
                }
            }
        }

        return cities;
    }

    //public async void OpenConnection()
    //{
    //    await using var dataSource = NpgsqlDataSource.Create(connectionString);
    //}

    //public void CreateTables()
    //{
    //    using var connection = new NpgsqlConnection(connectionString);
    //    connection.Open();

    //    using var command = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS WeatherData (id SERIAL PRIMARY KEY, city VARCHAR(255), country VARCHAR(255), lat VARCHAR(255), lon VARCHAR(255), weather JSONB, date DATE);", connection);
    //    command.ExecuteNonQuery();
    //}

    //public NpgsqlConnection Connection { get; set; } = new NpgsqlConnection("Host=localhost;Username=postgres;Password=password;Database=OpenWeather");
}