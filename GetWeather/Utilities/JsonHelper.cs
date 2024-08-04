using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeather.Utilities
{
    public static class JsonHelper
    {
        public static bool IsJsonArray(string jsonString)
        {
            try
            {
                var token = JToken.Parse(jsonString);
                return token.Type == JTokenType.Array;
            }
            catch (JsonReaderException)
            {
                // Handle incorrect JSON format here if necessary
                return false;
            }
        }

        public static bool IsJsonObject(string jsonString)
        {
            try
            {
                var token = JToken.Parse(jsonString);
                return token.Type == JTokenType.Object;
            }
            catch (JsonReaderException)
            {
                // Handle incorrect JSON format here if necessary
                return false;
            }
        }
    }
}
