using System;

public class Program
{
    public static void Main(string[] args)
    {
        if (!int.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        if (!int.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        if (a >= b)
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        for (int i = a; i < b; i++)
        {
            if (i % 2 == 0)
            {
                Console.WriteLine(i);
            }
        }
        
    }
}