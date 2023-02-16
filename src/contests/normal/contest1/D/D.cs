using System;

public class Program
{
    public static void Main()
    {
        string firstNum = Console.ReadLine();
        string secondNum = Console.ReadLine();
        int a;
        int b;
            
        if (!int.TryParse(firstNum, out a) || !int.TryParse(secondNum, out b))
        {
            // Если одна из полученных строк не может быть конвертирована в тип int
            // выводим "incorrect input" и завершаем программу
            Console.WriteLine("Incorrect input");
            return;
        }

        // "Из серии очевидно, что..." (C)
        if (a == b) Console.WriteLine("Equal");
        if (a > b) Console.WriteLine("First");
        if (a < b) Console.WriteLine("Second");
    }
}