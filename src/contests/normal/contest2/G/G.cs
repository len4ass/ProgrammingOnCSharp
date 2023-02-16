using System;

public partial class Program
{
    // Вычисление максимума трех чисел.
    private static double MaxOfThree(double a, double b, double c)
    {
        return Math.Max(a, Math.Max(b, c));
    }
}