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
            // Если полученные строки не могут быть конвертированы в тип double
            // выводим "incorrect input" и завершаем программу
            Console.WriteLine("Incorrect input");
            return;
        }

        //Нам нужен сектор между двумя окружностями
        double sumOfSquares = Math.Pow(a, 2) + Math.Pow(b, 2);
        Console.WriteLine((sumOfSquares <= 2.0D && sumOfSquares >= 1.0D) ? "True" : "False");
    } 
}