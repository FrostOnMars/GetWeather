using GetWeather.Models;
using GetWeather.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;

namespace GetWeather.Controllers;

public static class OpenWeatherController
{
    #region Public Methods

    /// <summary>
    /// Gets geo coordinate candidates for a city. On success, SelectedCities.Cities is populated.
    /// Returns ApiResult containing the parsed GeoDatum list (or error info).
    /// </summary>
    public static ApiResult<List<GeoDatum>> GetGeoCoordinates(string city, string fullUrl)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(city))
        {
            return ApiResult<List<GeoDatum>>.Fail(new ApiError(
                Message: "City must be provided (was null/empty).",
                Kind: ApiErrorKind.Validation,
                Url: fullUrl));
        }

        if (string.IsNullOrWhiteSpace(fullUrl))
        {
            return ApiResult<List<GeoDatum>>.Fail(new ApiError(
                Message: "URL must be provided (was null/empty).",
                Kind: ApiErrorKind.Validation));
        }

        var requestResult = ExecuteRequest(fullUrl);
        if (!requestResult.Success)
            return ApiResult<List<GeoDatum>>.Fail(requestResult.Error!);

        var parseResult = DeserializeGeoCoordinates(requestResult.Data!, fullUrl);

        // Preserve your side effect ONLY when parsing succeeded
        if (parseResult.Success && parseResult.Data is { Count: > 0 } geoList)
        {
            // Optional: Decide if you want to clear previous results before adding new ones
            // SelectedCities.Cities.Clear();

            AssignGeoCoordinates(geoList);
        }

        return parseResult;
    }

    /// <summary>
    /// Gets current weather for a location. Returns ApiResult containing CurrentWeather (or error info).
    /// </summary>
    public static ApiResult<CurrentWeather> GetWeatherData(LocationParameterModel location, string fullUrl)
    {
        if (location is null)
        {
            return ApiResult<CurrentWeather>.Fail(new ApiError(
                Message: "Location parameter model must be provided (was null).",
                Kind: ApiErrorKind.Validation,
                Url: fullUrl));
        }

        if (string.IsNullOrWhiteSpace(fullUrl))
        {
            return ApiResult<CurrentWeather>.Fail(new ApiError(
                Message: "URL must be provided (was null/empty).",
                Kind: ApiErrorKind.Validation));
        }

        var requestResult = ExecuteRequest(fullUrl);
        if (!requestResult.Success)
            return ApiResult<CurrentWeather>.Fail(requestResult.Error!);

        return DeserializeWeatherData(requestResult.Data!, fullUrl);
    }

    #endregion Public Methods

    #region Private Methods

    private static void AssignGeoCoordinates(List<GeoDatum> geoCoordinates)
    {
        foreach (var selectedCity in geoCoordinates.Select(city => new SelectedCity
        {
            Parameter = new LocationParameterModel
            {
                Lat = city.Lat.ToString(CultureInfo.CurrentCulture),
                Lon = city.Lon.ToString(CultureInfo.CurrentCulture),
                Country = city.Country ?? string.Empty,
                State = city.State ?? string.Empty
            },
            City = city
        }))
        {
            SelectedCities.Cities.Add(selectedCity);
        }
    }

    /// <summary>
    /// Parse geo coordinate JSON that may be either an array or object.
    /// Returns ApiResult with GeoDatum list or detailed error.
    /// </summary>
    private static ApiResult<List<GeoDatum>> DeserializeGeoCoordinates(string responseContent, string url)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return ApiResult<List<GeoDatum>>.Fail(new ApiError(
                    Message: "Geo coordinate response was empty.",
                    Kind: ApiErrorKind.EmptyResponse,
                    Url: url));
            }

            List<GeoDatum> geoCoordinatesList = [];

            if (JsonHelper.IsJsonArray(responseContent))
            {
                geoCoordinatesList = JsonConvert.DeserializeObject<List<GeoDatum>>(responseContent) ?? [];
            }
            else if (JsonHelper.IsJsonObject(responseContent))
            {
                var single = JsonConvert.DeserializeObject<GeoDatum>(responseContent);
                if (single is not null)
                    geoCoordinatesList.Add(single);
            }
            else
            {
                return ApiResult<List<GeoDatum>>.Fail(new ApiError(
                    Message: "Geo coordinate response was not valid JSON (expected array or object).",
                    Kind: ApiErrorKind.JsonFormat,
                    Url: url,
                    ResponseSnippet: Truncate(responseContent, 600)));
            }

            if (geoCoordinatesList.Count == 0)
            {
                return ApiResult<List<GeoDatum>>.Fail(new ApiError(
                    Message: "Geo coordinate JSON parsed, but contained no results.",
                    Kind: ApiErrorKind.DataMissing,
                    Url: url,
                    ResponseSnippet: Truncate(responseContent, 600)));
            }

            return ApiResult<List<GeoDatum>>.Ok(geoCoordinatesList);
        }
        catch (JsonException jex)
        {
            return ApiResult<List<GeoDatum>>.Fail(new ApiError(
                Message: "Failed to parse geo coordinate JSON.",
                Kind: ApiErrorKind.JsonFormat,
                Url: url,
                ResponseSnippet: Truncate(responseContent, 600),
                ExceptionType: jex.GetType().Name,
                ExceptionMessage: jex.Message));
        }
        catch (Exception ex)
        {
            return ApiResult<List<GeoDatum>>.Fail(new ApiError(
                Message: "Unexpected error while deserializing geo coordinates.",
                Kind: ApiErrorKind.Unknown,
                Url: url,
                ResponseSnippet: Truncate(responseContent, 600),
                ExceptionType: ex.GetType().Name,
                ExceptionMessage: ex.Message));
        }
    }

    /// <summary>
    /// Parse weather JSON into CurrentWeather and validate required fields.
    /// Returns ApiResult with CurrentWeather or detailed error.
    /// </summary>
    private static ApiResult<CurrentWeather> DeserializeWeatherData(string responseContent, string url)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return ApiResult<CurrentWeather>.Fail(new ApiError(
                    Message: "Weather response was empty.",
                    Kind: ApiErrorKind.EmptyResponse,
                    Url: url));
            }

            var weather = JsonConvert.DeserializeObject<CurrentWeather>(responseContent);

            // Validate required data (customize this as your model requires)
            if (weather?.Weather == null || weather.Weather.Count == 0)
            {
                return ApiResult<CurrentWeather>.Fail(new ApiError(
                    Message: "Weather payload parsed, but required field 'Weather' was missing or empty.",
                    Kind: ApiErrorKind.DataMissing,
                    Url: url,
                    ResponseSnippet: Truncate(responseContent, 600)));
            }

            return ApiResult<CurrentWeather>.Ok(weather);
        }
        catch (JsonException jex)
        {
            return ApiResult<CurrentWeather>.Fail(new ApiError(
                Message: "Failed to parse weather JSON.",
                Kind: ApiErrorKind.JsonFormat,
                Url: url,
                ResponseSnippet: Truncate(responseContent, 600),
                ExceptionType: jex.GetType().Name,
                ExceptionMessage: jex.Message));
        }
        catch (Exception ex)
        {
            return ApiResult<CurrentWeather>.Fail(new ApiError(
                Message: "Unexpected error while deserializing weather data.",
                Kind: ApiErrorKind.Unknown,
                Url: url,
                ResponseSnippet: Truncate(responseContent, 600),
                ExceptionType: ex.GetType().Name,
                ExceptionMessage: ex.Message));
        }
    }

    /// <summary>
    /// Executes an HTTP request and returns ApiResult with response body or diagnostic error.
    /// </summary>
    private static ApiResult<string> ExecuteRequest(string url)
    {
        try
        {
            var client = new RestClient(url);

            // If you pass a full URL to RestClient, keep request as default.
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);

            // Network or RestSharp-level failure
            if (response.ErrorException is not null)
            {
                return ApiResult<string>.Fail(new ApiError(
                    Message: $"Network/request error: {response.ErrorMessage ?? response.ErrorException.Message}",
                    Kind: ApiErrorKind.Network,
                    Url: url,
                    StatusCode: (int?)response.StatusCode,
                    ResponseSnippet: Truncate(response.Content ?? string.Empty, 600),
                    ExceptionType: response.ErrorException.GetType().Name,
                    ExceptionMessage: response.ErrorException.Message));
            }

            // Non-success HTTP status
            if (!response.IsSuccessful)
            {
                return ApiResult<string>.Fail(new ApiError(
                    Message: $"OpenWeather returned non-success HTTP status {(int)response.StatusCode} ({response.StatusCode}).",
                    Kind: ApiErrorKind.Http,
                    Url: url,
                    StatusCode: (int)response.StatusCode,
                    ResponseSnippet: Truncate(response.Content ?? string.Empty, 600)));
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                return ApiResult<string>.Fail(new ApiError(
                    Message: "OpenWeather returned an empty response body.",
                    Kind: ApiErrorKind.EmptyResponse,
                    Url: url,
                    StatusCode: (int)response.StatusCode));
            }

            return ApiResult<string>.Ok(response.Content!);
        }
        catch (Exception ex)
        {
            return ApiResult<string>.Fail(new ApiError(
                Message: "Unexpected error while executing OpenWeather request.",
                Kind: ApiErrorKind.Unknown,
                Url: url,
                ExceptionType: ex.GetType().Name,
                ExceptionMessage: ex.Message));
        }
    }

    private static string Truncate(string value, int maxChars)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
    }

    #endregion Private Methods
}