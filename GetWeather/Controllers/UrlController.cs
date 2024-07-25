using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetWeather.Models;

namespace GetWeather.Controllers
{
    public interface IUrlAssembler
    {
        string AssembleUrlParameters<T>(T input);
    }

    public abstract class UrlController
    {
        public virtual string BaseUrl { get; set; }
        public virtual string UrlParameters { get; set; }
        public virtual string FullUrl => $"{BaseUrl}?{UrlParameters}";
    }

    public class WeatherUrlController : UrlController, IUrlAssembler
    {
        private readonly LocationParameterModel _locationParameterModel;

        public WeatherUrlController(LocationParameterModel locationParameterModel)
        {
            _locationParameterModel = locationParameterModel;
        }

        public override string BaseUrl => "https://api.openweathermap.org/data/2.5/weather";

        public override string UrlParameters => AssembleUrlParameters(_locationParameterModel);

        public string AssembleUrlParameters<T>(T input)
        {
            if (input is not LocationParameterModel location)
            {
                throw new ArgumentException("Invalid input type");
            }
            return $"lat={location.Lat}&lon={location.Lon}&appid={AppConfig.Instance.ApiKey}";
        }
    }

    public class GeoDataUrlController : UrlController, IUrlAssembler
    {
        private readonly string _city;

        public GeoDataUrlController(string city)
        {
            _city = city;
        }

        public override string BaseUrl => "https://api.openweathermap.org/data/2.5/weather";

        public override string UrlParameters => AssembleUrlParameters(_city);

        public string AssembleUrlParameters<T>(T input)
        {
            if (input is not string city)
            {
                throw new ArgumentException("Invalid input type");
            }
            return city == null ? $"q=London,&limit=5&appid={AppConfig.Instance.ApiKey}" : $"q={city},&limit=5&appid={AppConfig.Instance.ApiKey}";
        }
    }
    public class DailyWeatherUrlController : UrlController, IUrlAssembler
    {
        //Template for future extensibility. To add this class, change the return statement and BaseUrl to match the new parameters.
        
        private readonly LocationParameterModel _locationParameterModel;

        public DailyWeatherUrlController(LocationParameterModel locationParameterModel)
        {
            _locationParameterModel = locationParameterModel;
        }

        public override string BaseUrl => "https://api.openDailyWeathermap.org/data/2.5/DailyWeather";

        public override string UrlParameters => AssembleUrlParameters(_locationParameterModel);

        public string AssembleUrlParameters<T>(T input)
        {
            if (input is not LocationParameterModel location)
            {
                throw new ArgumentException("Invalid input type");
            }
            return $"lat={location.Lat}&lon={location.Lon}&appid={AppConfig.Instance.ApiKey}";
        }
    }

}

