using System;
using System.Collections.Generic;
using System.Linq;

public partial class Program
{
    private static int GetCountGreaterThanValue(int[] array, double average)
    {
        int count = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > average)
            {
                count += 1;
            }
        }

        return count;
    }

    private static double GetAverage(int[] array)
    {
        return (double)array.Sum() / array.Length;
    }
    
    private static bool ValidateNumber(out int n)
    {
        if (!int.TryParse(Console.ReadLine(), out n))
        {
            return false;
        }

        if (n < 0)
        {
            return false;
        }

        return true;
    }
    
    private static bool ReadNumbers(int n, out int[] array)
    { 
        array = new int[n];
        for (int i = 0; i < n; i++)
        {
            if (!int.TryParse(Console.ReadLine(), out array[i]))
            {
                return false;
            }

            if (array[i] < 0)
            {
                return false;
            }
        }

        return true;
    }
}