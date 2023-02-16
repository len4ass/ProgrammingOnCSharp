using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var list = new List<int>();
        while (int.TryParse(Console.ReadLine(), out int a) && a != 0)
        {
            if (a < 190 || a > 250)
            {
                Console.WriteLine("Incorrect number");
                continue;
            }
            
            list.Add(a);
        }

        list = list.OrderBy(x => x).ToList();
        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine(list[i]);
        }
    }
}