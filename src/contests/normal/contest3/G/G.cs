using System;
using System.Linq;

public partial class Program
{
    private static double GetMin(double[] array)
    {
        return array.Min();
    }

    private static double GetAverage(double[] array)
    {
        return (double)array.Sum() / array.Length;
    }

    private static double GetMedian(double[] array)
    {
        Array.Sort(array);
        double median = array.Length % 2 != 0 ? array[array.Length / 2] : 
            ((array[array.Length / 2] + array[array.Length / 2 - 1]) / 2);
        
        return median;
    }
    
    private static double[] ReadNumbers(string line)
    {
        string[] lineSplit = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        double[] values = new double[lineSplit.Length];
        for (int i = 0; i < values.Length; i++)
        {
            double.TryParse(lineSplit[i], out values[i]);
        }

        return values;
    }
}