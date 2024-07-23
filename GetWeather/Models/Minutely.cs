using Newtonsoft.Json;

namespace GetWeather.Models;

public class Minutely
{
    [JsonProperty("dt")]
    public int Dt { get; set; }

    [JsonProperty("precipitation")]
    public int Precipitation { get; set; }
}