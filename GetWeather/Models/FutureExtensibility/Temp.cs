using Newtonsoft.Json;

namespace GetWeather.Models.FutureExtensibility;

public class Temp
{
    #region Public Properties

    [JsonProperty("day")] public float Day { get; set; }

    [JsonProperty("eve")] public float Eve { get; set; }

    [JsonProperty("max")] public float Max { get; set; }

    [JsonProperty("min")] public float Min { get; set; }

    [JsonProperty("morn")] public float Morn { get; set; }

    [JsonProperty("night")] public float Night { get; set; }

    #endregion Public Properties
}