using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeather.Models
{
    public class WeatherReturnModel
    {
        public string Temp { get; set; }
        public string FeelsLike { get; set; }

        //I have 11 data models that convert Json names into C# names. I would like to populate the models with the results from
        //the GetWeatherData method. First, the program gets Geolocation data from the GetGeoCoordinates method, which provides
        //Lat, Lon, City, Country, and State (properties of the LocationParameterModel). Next, I would like to use Lat & Lon
        //to fill a new model called WeatherReturnModel with the Temp and FeelsLike properties. (For now.)
        //Is there a better way?
        //Can I use one GetWeatherData method to fill multiple models at once?

       
        
    }
    
}
