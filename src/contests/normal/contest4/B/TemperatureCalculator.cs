using System;

internal static class TemperatureCalculator
{
    public static double FromCelsiusToFahrenheit(double celsiusTemperature)
    {
        if (celsiusTemperature >= -273.15)
        {
            return celsiusTemperature * 9 / 5 + 32;
        }

        throw new ArgumentException("Incorrect input");
    }

    public static double FromCelsiusToKelvin(double celsiusTemperature)
    {
        if (celsiusTemperature >= -273.15)
        {
            return celsiusTemperature + 273.15;
        }

        throw new ArgumentException("Incorrect input");
    }

    public static double FromFahrenheitToCelsius(double fahrenheitTemperature)
    {
        if (fahrenheitTemperature >= -459.67)
        {
            return (fahrenheitTemperature - 32) * 5 / 9;
        }
        
        throw new ArgumentException("Incorrect input");
    }

    public static double FromFahrenheitToKelvin(double fahrenheitTemperature)
    {
        if (fahrenheitTemperature >= -459.67)
        {
            return (fahrenheitTemperature - 32) * 5 / 9 + 273.15;
        }
        
        throw new ArgumentException("Incorrect input");
    }

    public static double FromKelvinToCelsius(double kelvinTemperature)
    {
        if (kelvinTemperature >= 0)
        {
            return kelvinTemperature - 273.15;
        }
        
        throw new ArgumentException("Incorrect input");
    }

    public static double FromKelvinToFahrenheit(double kelvinTemperature)
    {
        if (kelvinTemperature >= 0)
        {
            return (kelvinTemperature - 273.15) * 9 / 5 + 32;
        }
        
        throw new ArgumentException("Incorrect input");
    }
}