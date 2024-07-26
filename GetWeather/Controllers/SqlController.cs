using Npgsql;

namespace GetWeather.Controllers;

public class SqlController
{
    //TODO: Implement SQL connection, create and organize tables, and write SQL queries to store and retrieve weather data.
    
    #region Private Fields

    private string connectionString =
        "Host=PostgreSQL 16;Port=5432;Database=OpenWeather;User Id=postgres; Password=password;";

    #endregion Private Fields

    public async void OpenConnection()
    {
        await using var dataSource = NpgsqlDataSource.Create(connectionString);
    }
    public void CreateTables()
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS WeatherData (id SERIAL PRIMARY KEY, city VARCHAR(255), country VARCHAR(255), lat VARCHAR(255), lon VARCHAR(255), weather JSONB, date DATE);", connection);
        command.ExecuteNonQuery();
    }

    public void InsertData(string city, string country, string lat, string lon, string weather, string date)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("INSERT INTO WeatherData (city, country, lat, lon, weather, date) VALUES (@city, @country, @lat, @lon, @weather, @date);", connection);
        command.Parameters.AddWithValue("@city", city);
        command.Parameters.AddWithValue("@country", country);
        command.Parameters.AddWithValue("@lat", lat);
        command.Parameters.AddWithValue("@lon", lon);
        command.Parameters.AddWithValue("@weather", weather);
        command.Parameters.AddWithValue("@date", date);
        command.ExecuteNonQuery();
    }

    //public NpgsqlConnection Connection { get; set; } = new NpgsqlConnection("Host=localhost;Username=postgres;Password=password;Database=OpenWeather");
}