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

        if (a < 1000 || a > 9999)
        {
            // ghetto-code проверка на четырехзначность и отрицательность
            Console.WriteLine("Incorrect input");
            return;
        }

        // Строка наоборот
        string reverseFirstNum = "";
        for (int i = firstNum.Length - 1; i >= 0; --i)
        {
            reverseFirstNum += firstNum[i];
        }

        // Тернарка для упрощения вывода
        Console.WriteLine((firstNum == reverseFirstNum) ? "True" : "False");
    }
}