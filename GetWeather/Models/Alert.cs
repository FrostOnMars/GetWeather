using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models;

public class Alert
{
    [JsonProperty("sender_name"), CanBeNull]
    public string SenderName { get; set; }

    [JsonProperty("event"), CanBeNull]
    public string Event { get; set; }

    [JsonProperty("start")]
    public int Start { get; set; }

    [JsonProperty("end")]
    public int End { get; set; }

    [JsonProperty("description"), CanBeNull]
    public string Description { get; set; }

    [JsonProperty("tags"), CanBeNull]
    public object[] Tags { get; set; }
}