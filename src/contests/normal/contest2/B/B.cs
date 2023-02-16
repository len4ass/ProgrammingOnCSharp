using System;

public class Program
{
    public static void Main(string[] args)
    {
        int oddSum = 0;
        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out int a))
            {
                Console.WriteLine("Incorrect input");
                break;
            }
            
            if (a == 0)
            {
                Console.WriteLine(oddSum);
                break;
            }
            
            if (a % 2 != 0)
            {
                oddSum += a;
            }
        }
    }
}