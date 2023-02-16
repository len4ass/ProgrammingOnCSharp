using System;

namespace Peer
{
    /// <summary>
    /// Работа с генерацией чисел
    /// </summary>
    public class NumberGenerate
    {
        /// <summary>
        /// Генерирует число с заданным количеством цифр,
        /// используя строковую конкатенацию и метод <c>Next</c> класса <c>System.Random</c>.
        /// </summary>
        /// <param name="lenght">Количество цифр в числе</param>
        /// <returns>Число типа <c>long</c> с заданным количеством цифр</returns>
        public static long RandomLong(int lenght)
        {
            Random randomize = new Random();
            string generatedNumber = "";

            // Цикл для генерации строкового представление игрового числа.
            for (int i = 0; i < lenght; i++)
            {
                // Первая цифра должна быть в диапазоне [1, 9] эквивалентно [1, 10).
                if (i == 0)
                {
                    generatedNumber += randomize.Next(1, 10).ToString();
                    continue;
                }
                
                while (true)
                {
                    // Каждая последующая цифра не должна содержаться в промежуточной
                    // строке generatedNumber и должна быть в диапазоне [0, 9] эквивалентно [0, 10).
                    string generatedDigit = randomize.Next(0, 10).ToString();

                    if (!generatedNumber.Contains(generatedDigit))
                    {
                        generatedNumber += generatedDigit;
                        break;
                    }
                }
            }

            // Перевод строки в число типа long.
            long.TryParse(generatedNumber, out long randomLong);
            return randomLong;
        }
    }
}