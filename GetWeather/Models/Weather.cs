using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models;

public class Weather
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("main"), CanBeNull]
    public string Main { get; set; }

    [JsonProperty("description"), CanBeNull]
    public string Description { get; set; }

    [JsonProperty("icon"), CanBeNull]
    public string Icon { get; set; }
}