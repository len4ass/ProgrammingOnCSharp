using System;

internal class Citizen : IOptimist, IPessimist
{
    private int _value;
    
    public Citizen(int predictedValue)
    {
        _value = predictedValue;
    }
    
    public double GetForecast()
    {
        return _value * 1.1;
    }

    double IOptimist.GetForecast()
    {
        return _value * 2;
    }

    double IPessimist.GetForecast()
    {
        return _value * 0.6;
    }
    
    internal static Citizen Parse(string input)
    {
        _ = int.TryParse(input, out var val);
        if (val <= 0)
        {
            throw new ArgumentException("Incorrect citizen");
        }
        
        return new Citizen(val);
    }
}