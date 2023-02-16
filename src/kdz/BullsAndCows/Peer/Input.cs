using System;

namespace Peer
{
    /// <summary>
    /// Работает с вводом, выводом в консоли
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Считывает с консоли значение и передает его по ссылке в типе <c>int</c>.
        /// </summary>
        /// <param name="outputValue">При возврате метода содержит значение типа <c>int</c></param>
        /// <returns><c>true</c>, если получилось сконвертировать значение с консоли,
        /// иначе <c>false</c></returns>
        public static bool GetValue(out int outputValue)
        {
            if (!int.TryParse(Console.ReadLine(), out outputValue))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Считывает с консоли значение и передает его по ссылке в типе <c>long</c>.
        /// </summary>
        /// <param name="outputValue">При возврате метода содержит значение типа <c>long</c></param>
        /// <returns><c>true</c>, если получилось сконвертировать значение с консоли,
        /// иначе <c>false</c></returns>
        public static bool GetValue(out long outputValue)
        {
            if (!long.TryParse(Console.ReadLine(), out outputValue))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Генерирует число с заданной длиной с помощью метода <c>RandomLong</c>
        /// класса <c>NumberGenerate</c> и передает его по ссылке в типе <c>long</c>.
        /// </summary>
        /// <param name="numberLenght">Число для задания длины</param>
        /// <param name="numberRandom">При возврате метода содержит сгенерированное значение типа <c>long</c></param>
        public static void ControlNumberGeneration(int numberLenght, out long numberRandom)
        {
            numberRandom = NumberGenerate.RandomLong(numberLenght);
            Console.WriteLine($"Число успешно сгенерировано!");
        }

        /// <summary>
        /// Выводит на экран предложение о начале новой игры и считывает ввод.
        /// </summary>
        /// <returns><c>true</c>, если пользователь ввел 'Да', иначе <c>false</c></returns>
        public static bool ControlRun()
        {
            Console.WriteLine("Хотите начать заново?");
            Console.WriteLine("Введите 'Y', чтобы начать заново.");
            Console.WriteLine("Введите любую другую последовательность символов, чтобы завершить сессию.");
            Console.Write("Ввод: ");
            
            string userResult = Console.ReadLine();
            return userResult == "Y";
        }
        
        /// <summary>
        /// Просит пользователя ввести число от 1 до 10. Если ввод является верным, то завершает цикл
        /// и передает введенное значение по ссылке, иначе требует ввод до того момента, пока он не будет верным.
        /// </summary>
        /// <param name="numberLenght">При возврате метода содержит значение типа <c>int</c></param>
        public static void ControlNumberLenght(out int numberLenght)
        {
            while (true)
            {
                Console.Write("\nВведите количество цифр для загадываемого числа (от 1 до 10 включительно): ");
                bool isParsedAmount = GetValue(out int inputLenght);
                
                // Проверка ввода на валидность, продолжение цикла в случае валидности,
                // иначе переход на новую итерацию и запрос ввода.
                if (!isParsedAmount || inputLenght < 1 || inputLenght > 10)
                {
                    Console.WriteLine("Неверный ввод! Попробуйте заново.");
                    continue;
                }

                // Завершение цикла в случае валидности ввода.
                numberLenght = inputLenght;
                break;
            }
        }
        
        /// <summary>
        /// Просит пользователя ввести догадку насчет сгенерированного числа. Если догадка является числом верной длины,
        /// то если догадка полностью верна, цикл завершается и происходит вывод сообщения о победе; если же частично
        /// верна, то происходит вывод количества коров и быков. Иначе, пользователя просят снова ввести догадку, пока
        /// она не будет верным вводом. 
        /// </summary>
        /// <param name="numberLenght">Число для задания длины</param>
        /// <param name="numberRandom">Число, сгенерированное игрой</param>
        public static void ControlNumberGuesses(int numberLenght, long numberRandom)
        {
            while (true)
            {
                Console.Write($"\nВведите вашу {numberLenght}-значную догадку: ");
                bool isParsedValue = GetValue(out long numberGuessed);
                
                // Проверка ввода на валидность, продолжение цикла в случае валидности,
                // иначе переход на новую итерацию и запрос ввода.
                if (!isParsedValue || numberGuessed.ToString().Length != numberLenght)
                {
                    Console.WriteLine("Неверный ввод! Попробуйте заново.");
                    continue;
                }

                int cowsAmount = NumberAnalyze.GetCowGuesses(numberRandom, numberGuessed);
                int bullsAmount = NumberAnalyze.GetBullGuesses(numberRandom, numberGuessed);
                
                Console.WriteLine($"Угадано коров: {cowsAmount}");
                Console.WriteLine($"Угадано быков: {bullsAmount}");
                
                // Условие завершения цикла.
                if (numberGuessed == numberRandom)
                {
                    Console.WriteLine($"\nВы угадали! Загаданное число - {numberRandom}.\n");
                    break;
                }
            }
        }
    }
}