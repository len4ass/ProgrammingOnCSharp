using System;

public class Program
{
    public static void Main(string[] args)
    {
        if (!uint.TryParse(Console.ReadLine(), out uint a))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        int numSum = 0;
        while (a > 0)
        {
            numSum += (int)(a % 10);
            a /= 10;
        }
        
        Console.WriteLine(numSum);
    }
}