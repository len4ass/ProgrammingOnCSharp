using System;

internal class Program
{
    public static void Main(string[] args)
    {
        if (!short.TryParse(Console.ReadLine(), out short a) | !short.TryParse(Console.ReadLine(), out short b))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        Console.WriteLine(-(a | b) - 1);
    }
}