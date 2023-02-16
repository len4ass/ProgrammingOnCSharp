using System;

internal delegate double Calculate(int n);

internal class Program
{
    private static void Main()
    {
        string line = Console.ReadLine();
        double x = double.Parse(line);
        
        double sum = 0;
        for (int i = 1; i <= 5; i++)
        {
            double product = 1;
            for (int j = 1; j <= 5; j++)
            {
                product *= (i + 42) * x / (j * j);
            }

            sum += product;
        }

        Console.WriteLine($"{sum:F3}");
    }
}