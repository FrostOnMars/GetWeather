using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models;

public class WeatherDatum
{
    [JsonProperty("lat")]
    public float Lat { get; set; }

    [JsonProperty("lon")]
    public float Lon { get; set; }

    [JsonProperty("timezone"), CanBeNull]
    public string Timezone { get; set; }

    [JsonProperty("timezone_offset")]
    public int TimezoneOffset { get; set; }

    [JsonProperty("current"), CanBeNull]
    public Current Current { get; set; }

    [JsonProperty("minutely"), CanBeNull]
    public Minutely[] Minutely { get; set; }

    [JsonProperty("hourly"), CanBeNull]
    public Hourly[] Hourly { get; set; }

    [JsonProperty("daily"), CanBeNull]
    public Daily[] Daily { get; set; }

    [JsonProperty("alerts"), CanBeNull]
    public Alert[] Alerts { get; set; }
}