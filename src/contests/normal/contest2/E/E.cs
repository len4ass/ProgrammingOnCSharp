public partial class Program
{
    // Вычисление факториала.
    private static int Factorial(int value)
    {
        if (value == 0)
        {
            return 1;
        }
        
        int factorial = value;
        for (int i = 1; i < value; i++)
        {
            factorial *= i;
        }

        return factorial;
    }
    
    // Проверка входного числа на корректность.
    private static bool IsInputNumberCorrect(int number)
    {
        return number >= 0;
    }
}