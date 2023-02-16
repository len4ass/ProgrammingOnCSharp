using System;
using System.Linq;

internal partial class Program
{
    private static bool IsArrayLengthCorrect(int length)
    {
        return length > 0;
    }

    private static bool TryReadArray(int length, out int[] array)
    {
        array = new int[length];

        for (int i = 0; i < length; i++)
        {
            if (!int.TryParse(Console.ReadLine(), out array[i]))
            {
                return false;
            }
        }

        return true;
    }

    private static double AverageOfPositive(int[] array)
    {
        int count = 0;
        int sum = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > 0)
            {
                count += 1;
                sum += array[i];
            }
        }

        return (double) sum / count;
    }
}