using System;

internal class Program
{
    public static void Main(string[] args)
    {
        if (!double.TryParse(Console.ReadLine(), out double x))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        
        double a1 = x * x * x;
        double an = 0;
        double sum = a1;
        int n = 0;
        
        while (true)
        {
            an = a1 * (-x * x * x / ((2 * n + 2) * (2 * n + 3)));
            if (a1 == an)
            {
                break;
            }
            
            sum += an;
            a1 = an;
            n += 1;
        }
        Console.WriteLine(sum);
    }
}