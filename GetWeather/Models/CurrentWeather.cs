using GetWeather.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetWeather.Models.FutureExtensibility;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models;

public class CurrentWeather
{
    [JsonProperty("coord"), CanBeNull]
    public Coord Coord { get; set; }

    [JsonProperty("weather"), CanBeNull]
    public List<Weather> Weather { get; set; }

    [JsonProperty("base"), CanBeNull]
    public string Base { get; set; }

    [JsonProperty("main"), CanBeNull]
    public Main Main { get; set; }

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    [JsonProperty("wind"), CanBeNull]
    public Wind Wind { get; set; }

    [JsonProperty("rain"), CanBeNull]
    public Rain Rain { get; set; }

    [JsonProperty("clouds"), CanBeNull]
    public Clouds Clouds { get; set; }

    [JsonProperty("dt")]
    public int Dt { get; set; }

    [JsonProperty("sys"), CanBeNull]
    public Sys Sys { get; set; }

    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name"), CanBeNull]
    public string Name { get; set; }

    [JsonProperty("cod")]
    public int Cod { get; set; }
}

public class Coord
{
    [JsonProperty("lon")]
    public float Lon { get; set; }

    [JsonProperty("lat")]
    public float Lat { get; set; }
}

public class Main
{
    [JsonProperty("temp")]
    public float Temp { get; set; }

    [JsonProperty("feels_like")]
    public float FeelsLike { get; set; }

    [JsonProperty("temp_min")]
    public float TempMin { get; set; }

    [JsonProperty("temp_max")]
    public float TempMax { get; set; }

    [JsonProperty("pressure")]
    public int Pressure { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }

    [JsonProperty("sea_level")]
    public int SeaLevel { get; set; }

    [JsonProperty("grnd_level")]
    public int GrndLevel { get; set; }
}

public class Wind
{
    [JsonProperty("speed")]
    public float Speed { get; set; }

    [JsonProperty("deg")]
    public int Deg { get; set; }

    [JsonProperty("gust")]
    public float Gust { get; set; }
}

public class Rain
{
    [JsonProperty("1h")]
    public float OneHour { get; set; }
}

public class Clouds
{
    [JsonProperty("all")]
    public int All { get; set; }
}

public class Sys
{
    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("country"), CanBeNull]
    public string Country { get; set; }

    [JsonProperty("sunrise")]
    public int Sunrise { get; set; }

    [JsonProperty("sunset")]
    public int Sunset { get; set; }
}