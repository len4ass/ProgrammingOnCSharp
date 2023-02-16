using System;


public class Program
{
    public static void Main()
    {
        string firstNum = Console.ReadLine();
        int a;
            
        if (!int.TryParse(firstNum, out a))
        {
            // Если одна из полученных строк не может быть конвертирована в тип int
            // выводим "incorrect input" и завершаем программу
            Console.WriteLine("Incorrect input");
            return;
        }

        if (a < 0)
        {
            // Если число отрицательное
            // выводи "incorrect input" и завершаем программу
            Console.WriteLine("Incorrect input");
            return;
        }
        
        Console.WriteLine(a % 10);
    }
}