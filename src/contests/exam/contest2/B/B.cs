using System;

internal class Program
{
    private static void Main(string[] args)
    {
        int[] array = Array.ConvertAll(Console.ReadLine().Split(" "), int.Parse);
        double sum = 0;

        for (int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }

        sum *= 10;
        Console.WriteLine($"{sum:F3}");
    }
}