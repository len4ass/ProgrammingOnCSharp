using System;

namespace FileManager
{
    /// <summary>
    /// Базовый класс.
    /// </summary>
    class Program
    {
        static void Main()
        {
            while (true)
            {
                try
                {
                    var handler = new Input();
                    handler.Run();
                }
                catch (Exception)
                {
                    Console.WriteLine("\nПроизошла непредвиденная ошибка, попробуйте заново.");
                    Console.WriteLine("\nВведите любую последовательность символов, чтобы продолжить.");
                    Console.Write("Ввод: ");
                    _ = Console.ReadLine();
                }
            }
        }
    }
}
