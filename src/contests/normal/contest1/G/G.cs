using System;

public class Program
{
    public static void Main()
    {
        string firstNum = Console.ReadLine();
        double a;

        if (!double.TryParse(firstNum, out a))
        {
            // Если одна из полученных строк не может быть конвертирована в тип double
            // выводим "incorrect input" и завершаем программу
            Console.WriteLine("Incorrect input");
            return;
        }

        if (a < 0)
        {
            // Проверка на отрицательность
            Console.WriteLine("Incorrect input");
            return;
        }

        int temp = (int)(a * 10); // Умножаем на 10, чтобы перевести запятую и берем целую часть
        int result = temp % 10; // Получаем последний знак числа -> ответ
        Console.WriteLine(result);
    } 
}