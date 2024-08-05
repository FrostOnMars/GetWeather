using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GetWeather.Utilities;

public static class JsonHelper
{
    #region Public Methods

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

    #endregion Public Methods
}