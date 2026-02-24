using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeather.Controllers;

public sealed record ApiError(
    string Message,
    ApiErrorKind Kind,
    string? Url = null,
    int? StatusCode = null,
    string? ResponseSnippet = null,
    string? ExceptionType = null,
    string? ExceptionMessage = null)
{
    public override string ToString()
        => $"{Kind}: {Message}"
           + (Url is null ? "" : $"\nURL: {Url}")
           + (StatusCode is null ? "" : $"\nHTTP: {StatusCode}")
           + (ExceptionType is null ? "" : $"\nException: {ExceptionType}: {ExceptionMessage}")
           + (ResponseSnippet is null ? "" : $"\nResponse (snippet): {ResponseSnippet}");
}

public enum ApiErrorKind
{
    Validation,
    Network,
    Http,
    EmptyResponse,
    JsonFormat,
    DataMissing,
    Unknown
}

public sealed record ApiResult<T>(bool Success, T? Data, ApiError? Error)
{
    public static ApiResult<T> Ok(T data) => new(true, data, null);
    public static ApiResult<T> Fail(ApiError error) => new(false, default, error);
}