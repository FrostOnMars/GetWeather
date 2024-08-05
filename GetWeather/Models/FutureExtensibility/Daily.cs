using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models.FutureExtensibility;

public class Daily
{
    #region Public Properties

    [JsonProperty("clouds")] public int Clouds { get; set; }

    [JsonProperty("dew_point")] public float DewPoint { get; set; }

    [JsonProperty("dt")] public int Dt { get; set; }

    [JsonProperty("feels_like")]
    [CanBeNull]
    public FeelsLike FeelsLike { get; set; }

    [JsonProperty("humidity")] public int Humidity { get; set; }

    [JsonProperty("moon_phase")] public float MoonPhase { get; set; }

    [JsonProperty("moonrise")] public int Moonrise { get; set; }

    [JsonProperty("moonset")] public int Moonset { get; set; }

    [JsonProperty("pop")] public float Pop { get; set; }

    [JsonProperty("pressure")] public int Pressure { get; set; }

    [JsonProperty("rain")] public float Rain { get; set; }

    [JsonProperty("summary")][CanBeNull] public string Summary { get; set; }

    [JsonProperty("sunrise")] public int Sunrise { get; set; }

    [JsonProperty("sunset")] public int Sunset { get; set; }

    [JsonProperty("temp")][CanBeNull] public Temp Temp { get; set; }

    [JsonProperty("uvi")] public float Uvi { get; set; }

    [JsonProperty("weather")][CanBeNull] public Weather[] Weather { get; set; }

    [JsonProperty("wind_deg")] public int WindDeg { get; set; }

    [JsonProperty("wind_gust")] public float WindGust { get; set; }

    [JsonProperty("wind_speed")] public float WindSpeed { get; set; }

    #endregion Public Properties
}