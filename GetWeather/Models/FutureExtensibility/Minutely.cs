using Newtonsoft.Json;

namespace GetWeather.Models.FutureExtensibility;

public class Minutely
{
    #region Public Properties

    [JsonProperty("dt")] public int Dt { get; set; }

    [JsonProperty("precipitation")] public int Precipitation { get; set; }

    #endregion Public Properties
}