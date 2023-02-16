using System;
using System.Net.Mail;

internal class Program
{
    public static int Recursion(int n)
    {
        if (n == 0)
        {
            return -1;
        }

        if (n == 1)
        {
            return 1;
        }
        
        return Recursion(n - 2) * Recursion(n - 2) - 3 * Recursion(n - 1);
    }
    
    
    public static void Main(string[] args)
    {
        if (!int.TryParse(Console.ReadLine(), out int a) || a <= 0)
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        
        Console.WriteLine(Recursion(a));
    }
}