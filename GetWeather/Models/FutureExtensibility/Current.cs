using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models.FutureExtensibility;

public class Current
{
    [JsonProperty("dt")]
    public int Dt { get; set; }

    [JsonProperty("sunrise")]
    public int Sunrise { get; set; }

    [JsonProperty("sunset")]
    public int Sunset { get; set; }

    [JsonProperty("temp")]
    public float Temp { get; set; }

    [JsonProperty("feels_like")]
    public float FeelsLike { get; set; }

    [JsonProperty("pressure")]
    public int Pressure { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }

    [JsonProperty("dew_point")]
    public float DewPoint { get; set; }

    [JsonProperty("uvi")]
    public float Uvi { get; set; }

    [JsonProperty("clouds")]
    public int Clouds { get; set; }

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    [JsonProperty("wind_speed")]
    public float WindSpeed { get; set; }

    [JsonProperty("wind_deg")]
    public int WindDeg { get; set; }

    [JsonProperty("wind_gust")]
    public float WindGust { get; set; }

    [JsonProperty("weather"), CanBeNull]
    public Weather[] Weather { get; set; }
}