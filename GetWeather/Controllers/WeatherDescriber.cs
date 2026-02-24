using GetWeather.Models;
using GetWeather.Models.FutureExtensibility;

namespace GetWeather.Controllers;

// <summary>
// This class describes the weather conditions in a human-readable format. It handles unit
// conversions, cloud percentage, wind speed, and precipitation.
// </summary>
public static class WeatherDescriber
{
    #region Private Fields

    private static readonly Dictionary<LengthUnit, double> LengthConversionFactors = new()
    {
        { LengthUnit.Meter, 1.0 },
        { LengthUnit.Kilometer, 1000.0 },
        { LengthUnit.Centimeter, 0.01 },
        { LengthUnit.Millimeter, 0.001 },
        { LengthUnit.Inch, 0.0254 },
        { LengthUnit.Foot, 0.3048 },
        { LengthUnit.Yard, 0.9144 },
        { LengthUnit.Mile, 1609.34 }
    };

    // Conversion factors relative to Pascals (base unit).
    // Moved to a field so we don't allocate a dictionary every call.
    private static readonly Dictionary<PressureUnit, double> PressureConversionFactors = new()
    {
        { PressureUnit.Pascal, 1.0 },
        { PressureUnit.Bar, 1e5 },
        { PressureUnit.Atmosphere, 101325 },
        { PressureUnit.PoundsPerSquareInch, 6894.76 }
    };

    #endregion Private Fields

    #region Private Methods

    private static int GetSignificantDigits(double value)
    {
        var valueStr = value.ToString("G"); // "G" format removes trailing zeroes
        valueStr = valueStr.Trim('0').Replace(".", "").Replace("-", "");
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
        if (rain == null || rain.OneHour == 0)
            return string.Empty;

        var rainInInches = Convert(rain.OneHour, LengthUnit.Millimeter, LengthUnit.Inch);
        return $"{rainInInches} {LengthUnit.Inch} per hour";
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

    public static string DescribeFeelsLike(float feelsLike)
    {
        var temperature = GetCurrentTemperatureByTimeOfDay(feelsLike);

        var description = temperature.TimeofDay switch
        {
            "morning" => temperature.Temperature switch
            {
                <= -10 => "brisk morning, comrade. Perfect day to question life choices... maybe over coffee with vodka?",
                > -10 and <= 0 => "colder than your ex's heart, or the other side of the bed.",
                > 0 and <= 10 => "frigid enough to freeze your soul. At least your heart won't notice.",
                > 10 and <= 20 => "cold enough to see your breath. Great, now you can watch your misery in real-time.",
                > 20 and <= 30 => "chilly with a chance of shivering. Just like your dreams of a warm vacation.",
                > 30 and <= 40 => "cool enough to need a jacket. Because clearly, life isn’t hard enough already.",
                > 40 and <= 50 => "brisk but bearable. Unlike your workload.",
                > 50 and <= 60 => "pleasantly cool. Almost as refreshing as your bank account balance.",
                > 60 and <= 70 => "like a perfect spring day. Too bad you’ll be stuck inside working.",
                > 70 and <= 80 => "just right... nothing, actually. Like your dating life.",
                > 80 and <= 90 => "warm enough for shorts. Because why not make people question your fashion choices too?",
                > 90 and <= 100 => "hot enough to fry an egg on the sidewalk. Good luck serving it to anyone.",
                > 100 and <= 110 => "scorching, like the surface of the sun. Is this app working? Could this be Kelvin?",
                > 110 and <= 120 => "Hell itself. Even Satan would call in sick.",
                _ => "unbearably hot. Seek shelter--and maybe a new home."
            },

            "afternoon" => temperature.Temperature switch
            {
                <= -10 => "morally wrong amount of cold",
                > -10 and <= 0 => "colder than your ex's heart",
                > 0 and <= 10 => "frigid enough to freeze your soul",
                > 10 and <= 20 => "don't lick any poles",
                > 20 and <= 30 => "cold enough to see your breath",
                > 30 and <= 40 => "cool enough to need a jacket",
                > 40 and <= 50 => "brisk but bearable",
                > 50 and <= 60 => "pleasantly cool",
                > 60 and <= 70 => "like a perfect spring day",
                > 70 and <= 80 => "just right... nothing, actually",
                > 80 and <= 90 => "warm enough for shorts",
                > 90 and <= 100 => "hot enough to fry an egg on the sidewalk",
                > 100 and <= 110 => "scorching, like the surface of the sun",
                > 110 and <= 120 => "Hell itself",
                _ => "unbearably hot, seek shelter!"
            },

            "evening" => temperature.Temperature switch
            {
                <= -10 => "like stepping into a cryogenic freezer. Great if you’re into instant frostbite.",
                > -10 and <= 0 => "colder than what it feels like to chew Five gum.",
                > 0 and <= 10 => "frigid enough to make the mosquitoes suffer.",
                > 10 and <= 20 => "cold enough that your breath freezes before you finish exhaling. Lovely.",
                > 20 and <= 30 => "chilly with a side of why-did-I-even-bother-coming-out-here.",
                > 30 and <= 40 => "cool enough to pretend you’re in a winter sports commercial. If only.",
                > 40 and <= 50 => "brisk but bearable. Just like your tolerance for adulting.",
                > 50 and <= 60 => "pleasantly cool, like your attitude after a long day at work.",
                > 60 and <= 70 => "like a perfect spring evening, which means your allergies are just getting started.",
                > 70 and <= 80 => "just right for a barbecue. Too bad you’re too tired to host one.",
                > 80 and <= 90 => "warm enough for shorts, assuming you still have the energy to find them.",
                > 90 and <= 100 => "hot enough to melt your ice cream before you get the chance to complain.",
                > 100 and <= 110 => "like walking into an oven preheated for your evening roast.",
                > 110 and <= 120 => "the kind of heat that makes even cacti think twice. Seriously, stay hydrated.",
                _ => "so unbearably hot, you start wondering if you missed the memo about spontaneous combustion."
            },

            _ => throw new InvalidOperationException("Invalid time of day")
        };

        return $"{temperature}: {description}";
    }

    public static (double Temperature, string TimeofDay) GetCurrentTemperatureByTimeOfDay(float feelsLike)
    {
        var temperature = Convert(feelsLike, TemperatureUnit.Kelvin, TemperatureUnit.Fahrenheit);

        var timeOfDay = DateTime.Now.Hour switch
        {
            >= 0 and <= 11 => "morning",
            > 11 and <= 17 => "afternoon",
            _ => "evening"
        };

        return (Math.Round(temperature), timeOfDay);
    }

    public static double Convert(double value, LengthUnit inputUnit, LengthUnit outputUnit)
    {
        if (inputUnit == outputUnit) return value;

        var inputFactor = LengthConversionFactors[inputUnit];
        var outputFactor = LengthConversionFactors[outputUnit];
        return value * inputFactor / outputFactor;
    }

    public static double Convert(double value, TemperatureUnit inputUnit, TemperatureUnit outputUnit)
    {
        if (inputUnit == outputUnit) return value;

        // Convert input to Celsius
        var celsiusValue = inputUnit switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => (value - 32) * 5.0 / 9.0,
            TemperatureUnit.Kelvin => value - 273.15,
            _ => throw new InvalidOperationException($"Unsupported temperature unit: {inputUnit}")
        };

        // Convert from Celsius to output unit
        return outputUnit switch
        {
            TemperatureUnit.Celsius => celsiusValue,
            TemperatureUnit.Fahrenheit => celsiusValue * 9.0 / 5.0 + 32,
            TemperatureUnit.Kelvin => celsiusValue + 273.15,
            _ => throw new InvalidOperationException($"Unsupported temperature unit: {outputUnit}")
        };
    }

    public static double Convert(double value, PressureUnit inputUnit, PressureUnit outputUnit)
    {
        if (inputUnit == outputUnit) return value;

        var inputFactor = PressureConversionFactors[inputUnit];
        var outputFactor = PressureConversionFactors[outputUnit];
        return value * inputFactor / outputFactor;
    }

    // --------------------------------------------------------------------
    // Original generic Convert<T1,T2> for compatibility--in case refactor broke Convert() method.
    // This is now just a small dispatcher (no unsafe casts, no typeof checks).
    // --------------------------------------------------------------------
    public static double Convert<T1, T2>(double value, T1 inputUnit, T2 outputUnit)
    {
        // If this gets called with reference types elsewhere, guard it.
        ArgumentNullException.ThrowIfNull((object?)inputUnit, nameof(inputUnit));
        ArgumentNullException.ThrowIfNull((object?)outputUnit, nameof(outputUnit));

        // Early return when both refer to same unit value/type
        if (Equals(inputUnit, outputUnit) && inputUnit?.GetType() == outputUnit?.GetType())
            return value;

        return ((object)inputUnit!, (object)outputUnit!) switch
        {
            (LengthUnit inLen, LengthUnit outLen) => Convert(value, inLen, outLen),
            (TemperatureUnit inTemp, TemperatureUnit outTemp) => Convert(value, inTemp, outTemp),
            (PressureUnit inPressure, PressureUnit outPressure) => Convert(value, inPressure, outPressure),

            _ => throw new InvalidOperationException(
                $"Cannot convert from {inputUnit} ({inputUnit!.GetType().Name}) to {outputUnit} ({outputUnit!.GetType().Name}).")
        };
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

    public static string ConvertDegreesToDirection(double degrees)
    {
        degrees %= 360;
        if (degrees < 0) degrees += 360;

        string[] directions =
        {
            "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE",
            "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW"
        };

        var index = (int)((degrees / 22.5) + 0.5) % 16;
        return directions[index];
    }

    #endregion Public Methods
}

public enum LengthUnit
{
    Meter,
    Kilometer,
    Centimeter,
    Millimeter,
    Inch,
    Foot,
    Yard,
    Mile
}

public enum TemperatureUnit
{
    Celsius,
    Fahrenheit,
    Kelvin
}

public enum PressureUnit
{
    Pascal,
    Bar,
    Atmosphere,
    PoundsPerSquareInch
}