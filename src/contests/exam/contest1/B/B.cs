using System;

internal class Program
{
    public static void Main(string[] args)
    {
        if (!uint.TryParse(Console.ReadLine(), out uint a))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        
        if (!uint.TryParse(Console.ReadLine(), out uint b))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        
        if (!uint.TryParse(Console.ReadLine(), out uint c))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        
        if (!uint.TryParse(Console.ReadLine(), out uint d))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        double result = (double)c * (a + b) / 2;
        Console.WriteLine(result);
    }
}