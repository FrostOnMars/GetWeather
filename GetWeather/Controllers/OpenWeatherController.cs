using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace GetWeather.Controllers
{
    public class OpenWeatherController
    {
        //call the class and get its data
        public void GetWeather()
        {
            var client = new RestClient(AppConfig.Instance.FullUrl);
            var request = new RestRequest(AppConfig.Instance.FullUrl, Method.Get);

            _ = request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
            }
        }
    }
}
