using System;

public class Program
{
    // Берет double, модуль от него, чтобы работать с положительными числами
    // вычисляет нижнюю границу и выполняет логику по поиску ближайшего четного или ближайшего нечетного
    public static void Round(double toRound)
    {
        double toRoundModulo = (toRound < 0) ? -toRound : toRound;
        int toRoundIntPart = (int)toRoundModulo;
        double pointDifference = toRoundModulo - toRoundIntPart;

        if (pointDifference == 0.5f)
        {
            int roundResultModulo = (toRoundIntPart % 2 == 1) ? toRoundIntPart : (toRoundIntPart + 1);
            Console.WriteLine((toRound < 0) ? -roundResultModulo : roundResultModulo);
        }
        else
        {
            double roundResultModulo = Math.Round(toRound);
            Console.WriteLine(roundResultModulo);
        }
    }
    
    public static void Main()
    {
        string firstNum = Console.ReadLine();
        double a;

        if (!double.TryParse(firstNum, out a))
        {
            // Если полученные строки не могут быть конвертированы в тип double
            // выводим "incorrect input" и завершаем программу
            Console.WriteLine("Incorrect input");
            return;
        }

        Round(a);
    } 
}