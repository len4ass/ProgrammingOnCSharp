using System;

public class Program
{
    public static void Main()
    {
        string firstNum = Console.ReadLine();
        string secondNum = Console.ReadLine();
        double a;
        double b;

        if (!double.TryParse(firstNum, out a) || !double.TryParse(secondNum, out b))
        {
            // Если одна из полученных строк не может быть конвертирована в тип double
            // выводим "incorrect input" и завершаем программу
            Console.WriteLine("Incorrect input");
            return;
        }

        if (a < 0 || b < 0)
        {
            // ghetto-code проверка на четырехзначность и отрицательность
            Console.WriteLine("Incorrect input");
            return;
        }

        // Теорема пифагора
        double sumOfSquares = Math.Pow(a, 2) + Math.Pow(b, 2);
        double hypotenuse = Math.Sqrt(sumOfSquares);
        // 0:N6 для шести знаков (больше = лучше)
        Console.WriteLine("{0:N6}", hypotenuse);
    } 
}
