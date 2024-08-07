﻿using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models.FutureExtensibility;

public class Weather
{
    #region Public Properties

    [JsonProperty("description")]
    [CanBeNull]
    public string Description { get; set; }

    [JsonProperty("icon")][CanBeNull] public string Icon { get; set; }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("main")][CanBeNull] public string Main { get; set; }

    #endregion Public Properties
}