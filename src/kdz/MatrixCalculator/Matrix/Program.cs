using System;
using System.Globalization;
using System.Threading;

namespace Matrix
{
    /// <summary>
    /// Класс для инициализации программы и осуществления повторного решения.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Метод, работающий с основными вызовами.
        /// </summary>
        static void Start()
        {
            // Осуществляет работу программы и повтор решения.
            while (true)
            {
                Input.GetMatrixOperation();
                Input.GetMatrixInputType();
                Input.RunMethod();

                if (!Input.RunAgain())
                {
                    break;
                }
            }
        }
        
        /// <summary>
        /// Запускает программу.
        /// </summary>
        static void Main()
        {
            // Ставим английскую локаль для потока, в котором работает программа (чтобы генерализировать парсинг).
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            // Не благодарите за такую придумку.
            while (true)
            {
                try
                {
                    Start();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Произошла ошибка. Пожалуйста, начните сессию заново.");
                }
            }
        }
    }
}