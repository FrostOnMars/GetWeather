using GetWeather.Models.FutureExtensibility;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models;

public class Clouds
{
    #region Public Properties

    [JsonProperty("all")] public int All { get; set; }

    #endregion Public Properties
}

public class Coord
{
    #region Public Properties

    [JsonProperty("lat")] public float Lat { get; set; }

    [JsonProperty("lon")] public float Lon { get; set; }

    #endregion Public Properties
}

public class CurrentWeather
{
    #region Public Properties

    [JsonProperty("base")][CanBeNull] public string Base { get; set; }

    [JsonProperty("clouds")][CanBeNull] public Clouds Clouds { get; set; }

    [JsonProperty("cod")] public int Cod { get; set; }

    [JsonProperty("coord")][CanBeNull] public Coord Coord { get; set; }

    [JsonProperty("dt")] public int Dt { get; set; }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("main")][CanBeNull] public Main Main { get; set; }

    [JsonProperty("name")][CanBeNull] public string Name { get; set; }

    [JsonProperty("rain")][CanBeNull] public Rain Rain { get; set; }

    [JsonProperty("sys")][CanBeNull] public Sys Sys { get; set; }

    [JsonProperty("timezone")] public int Timezone { get; set; }

    [JsonProperty("visibility")] public int Visibility { get; set; }

    [JsonProperty("weather")][CanBeNull] public List<Weather> Weather { get; set; }

    [JsonProperty("wind")][CanBeNull] public Wind Wind { get; set; }

    #endregion Public Properties
}

public class Main
{
    #region Public Properties

    [JsonProperty("feels_like")] public float FeelsLike { get; set; }

    [JsonProperty("grnd_level")] public int GrndLevel { get; set; }

    [JsonProperty("humidity")] public int Humidity { get; set; }

    [JsonProperty("pressure")] public int Pressure { get; set; }

    [JsonProperty("sea_level")] public int SeaLevel { get; set; }

    [JsonProperty("temp")] public float Temp { get; set; }

    [JsonProperty("temp_max")] public float TempMax { get; set; }

    [JsonProperty("temp_min")] public float TempMin { get; set; }

    #endregion Public Properties
}

public class Rain
{
    #region Public Properties

    [JsonProperty("1h")] public float OneHour { get; set; }

    #endregion Public Properties
}

public class Wind
{
    #region Public Properties

    [JsonProperty("deg")] public int Deg { get; set; }

    [JsonProperty("gust")] public float Gust { get; set; }

    [JsonProperty("speed")] public float Speed { get; set; }

    #endregion Public Properties
}

public class Sys
{
    #region Public Properties

    [JsonProperty("country")][CanBeNull] public string Country { get; set; }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("sunrise")] public int Sunrise { get; set; }

    [JsonProperty("sunset")] public int Sunset { get; set; }

    [JsonProperty("type")] public int Type { get; set; }

    #endregion Public Properties
}