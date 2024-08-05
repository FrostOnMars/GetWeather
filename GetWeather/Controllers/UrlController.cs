using GetWeather.Models;

namespace GetWeather.Controllers;

public interface IUrlAssembler
{
    #region Public Methods

    string AssembleUrlParameters<T>(T input);

    #endregion Public Methods
}
public abstract class UrlController
{
    #region Public Properties

    public virtual string BaseUrl { get; set; }
    public virtual string FullUrl => $"{BaseUrl}?{UrlParameters}";
    public virtual string UrlParameters { get; set; }

    #endregion Public Properties
}
public class GeoDataUrlController : UrlController, IUrlAssembler
{
    #region Private Fields

    private readonly string _city;

    #endregion Private Fields

    #region Public Constructors

    public GeoDataUrlController(string city)
    {
        _city = city;
    }

    #endregion Public Constructors

    #region Public Methods

    public string AssembleUrlParameters<T>(T input)
    {
        if (input is not string city) throw new ArgumentException("Invalid input type");
        return city == null
            ? $"q=London,&limit=5&appid={AppConfig.Instance.ApiKey}"
            : $"q={city},&limit=5&appid={AppConfig.Instance.ApiKey}";
    }

    #endregion Public Methods

    #region Public Properties

    public override string BaseUrl => "http://api.openweathermap.org/geo/1.0/direct";

    public override string UrlParameters => AssembleUrlParameters(_city);

    #endregion Public Properties
}
public class WeatherUrlController : UrlController, IUrlAssembler
{
    #region Private Fields

    private readonly LocationParameterModel _locationParameterModel;

    #endregion Private Fields

    #region Public Constructors

    public WeatherUrlController(LocationParameterModel locationParameterModel)
    {
        _locationParameterModel = locationParameterModel;
    }

    #endregion Public Constructors

    #region Public Methods

    public string AssembleUrlParameters<T>(T input)
    {
        if (input is not LocationParameterModel location) throw new ArgumentException("Invalid input type");
        return $"lat={location.Lat}&lon={location.Lon}&appid={AppConfig.Instance.ApiKey}";
    }

    #endregion Public Methods

    #region Public Properties

    public override string BaseUrl => "https://api.openweathermap.org/data/2.5/weather";

    public override string UrlParameters => AssembleUrlParameters(_locationParameterModel);

    #endregion Public Properties
}
public class DailyWeatherUrlController : UrlController, IUrlAssembler
{
    //Template for future extensibility. To add this class, change the return statement and BaseUrl to match the new parameters.

    #region Private Fields

    private readonly LocationParameterModel _locationParameterModel;

    #endregion Private Fields

    #region Public Constructors

    public DailyWeatherUrlController(LocationParameterModel locationParameterModel)
    {
        _locationParameterModel = locationParameterModel;
    }

    #endregion Public Constructors

    #region Public Methods

    public string AssembleUrlParameters<T>(T input)
    {
        if (input is not LocationParameterModel location) throw new ArgumentException("Invalid input type");
        return $"lat={location.Lat}&lon={location.Lon}&appid={AppConfig.Instance.ApiKey}";
    }

    #endregion Public Methods

    #region Public Properties

    public override string BaseUrl => "https://api.openDailyWeathermap.org/data/2.5/DailyWeather";

    public override string UrlParameters => AssembleUrlParameters(_locationParameterModel);

    #endregion Public Properties
}