using GetWeather.Models;

namespace GetWeather.Controllers;

// This class describes the weather conditions in a human-readable format. It handles unit
// conversions, cloud percentage, wind speed, and precipitation.
public static class WeatherDescriber
{
    #region Private Fields

    private static readonly Dictionary<string, double> LengthConversionFactors = new()
    {
        { "meter", 1.0 },
        { "kilometer", 1000.0 },
        { "centimeter", 0.01 },
        { "millimeter", 0.001 },
        { "inch", 0.0254 },
        { "foot", 0.3048 },
        { "yard", 0.9144 },
        { "mile", 1609.34 }
    };

    #endregion Private Fields

    #region Private Methods

    private static int GetSignificantDigits(double value)
    {
        // Convert the value to string to count significant digits
        var valueStr = value.ToString("G"); // "G" format removes trailing zeroes
        valueStr = valueStr.Trim('0').Replace(".", "").Replace("-", ""); // Remove non-significant characters

        return valueStr.Length;
    }

    private static double RoundToSignificantDigits(double value, int significantDigits)
    {
        if (value == 0) return 0;

        var scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(value))) + 1);
        return Math.Round(value / scale, significantDigits) * scale;
    }

    #endregion Private Methods

    #region Public Methods

    public static string AddPrecipitationText(Rain rain)
    {
        return $"{rain} mm (one hour)";
    }

    public static string ConvertCloudPercentToString(Clouds clouds)
    {
        var cloudPercent = clouds.All;
        return cloudPercent switch
        {
            >= 0 and <= 10 => "Clear",
            > 10 and <= 30 => "Mostly Clear",
            > 30 and <= 50 => "Partly Cloudy",
            > 50 and <= 70 => "Mostly Cloudy",
            > 70 and <= 90 => "Cloudy",
            _ => "Overcast"
        };
    }

    public static double ConvertUnitOfMeasurement<T1, T2>(double value, T1 input, T2 output)
    {
        var inputUnit = input.ToString().ToLower();
        var outputUnit = output.ToString().ToLower();

        if (!LengthConversionFactors.ContainsKey(inputUnit))
            throw new ArgumentException($"Unsupported input unit: {inputUnit}");

        if (!LengthConversionFactors.ContainsKey(outputUnit))
            throw new ArgumentException($"Unsupported output unit: {outputUnit}");

        // Convert the input value to the base unit (meter)
        var valueInBaseUnit = value * LengthConversionFactors[inputUnit];

        // Convert the value from the base unit (meter) to the output unit
        var convertedValue = valueInBaseUnit / LengthConversionFactors[outputUnit];

        // Automatically determine and apply significant digits
        var significantDigits = GetSignificantDigits(value);
        convertedValue = RoundToSignificantDigits(convertedValue, significantDigits);

        return convertedValue;
    }

    public static string ConvertWindSpeedToBeaufortScale(Wind wind)
    {
        var windSpeed = wind.Speed;
        return windSpeed switch
        {
            >= 0 and <= 1 => "Calm",
            > 1 and <= 3 => "Light Air",
            > 3 and <= 7 => "Light Breeze",
            > 7 and <= 12 => "Gentle Breeze",
            > 12 and <= 18 => "Moderate Breeze",
            > 18 and <= 24 => "Fresh Breeze",
            > 24 and <= 31 => "Strong Breeze",
            > 31 and <= 38 => "Moderate Gale",
            > 38 and <= 46 => "Gale",
            > 46 and <= 54 => "Strong Gale",
            > 54 and <= 63 => "Storm",
            > 63 and <= 72 => "Violent Storm",
            _ => "Hurricane"
        };
    }

    #endregion Public Methods
}