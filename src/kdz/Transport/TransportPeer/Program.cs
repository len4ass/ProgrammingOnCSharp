using System;
using System.Linq;
using EKRLib;

namespace TransportPeer
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    internal static class Program
    {
        private const string PathCars = "../../../../Cars.txt";
        private const string PathMotorBoats = "../../../../MotorBoats.txt";

        /// <summary>
        /// Генерация транспорта и запись в файлы.
        /// </summary>
        private static void Run()
        {
            // Генерируем транспорт.
            var generator = new Generator();
            var allTransport = generator.GetRandomTransport();
            
            // Выделяем объекты являющиеся типом Car с помощью LINQ.
            var cars = allTransport.Where(transport => transport is Car);
            
            // Выделяем объекты являющиеся типом MotorBoat с помощью LINQ.
            var motorboats = allTransport.Where(transport => transport is MotorBoat);
            
            // Cast к типам наследникам не происходит (Q10).
            // ToString() вызывается (имплицитно) через ссылку базового класса, поскольку тип IEnumerable<Transport>.
            var carsString = string.Join("\n", cars);
            var motorboatsString = string.Join("\n", motorboats);
            
            // Запись результата в файлы.
            FileWorker.WriteFile(PathCars, carsString);
            FileWorker.WriteFile(PathMotorBoats, motorboatsString);
        }

        /// <summary>
        /// Спрашивает пользователя о повторе решения.
        /// </summary>
        /// <returns><c>true</c>, если пользователь ввел Y или y; иначе <c>false</c>.</returns>
        private static bool Repeat()
        {
            Console.WriteLine("\nХотите повторить работу программы?");
            Console.WriteLine("Введите Y или y, чтобы начать заново.");
            Console.WriteLine("Иначе введите любую другую последовательность символов.");
            Console.Write("\nВвод: ");

            string input = Console.ReadLine();
            Console.WriteLine();
            
            return input is "Y" or "y";
        }

        /// <summary>
        /// Исполнение программы.
        /// </summary>
        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("Генерация транспорта...");
                Run();

                if (!Repeat())
                {
                    break;
                }
            }
        }
    }
}