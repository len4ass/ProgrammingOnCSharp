namespace Peer
{
    /// <summary>
    /// Класс для анализа числа, загаданного игрой, и числа, введенного пользователем
    /// </summary>
    public class NumberAnalyze
    {
        /// <summary>
        /// Считает количество индексных совпадений.
        /// </summary>
        /// <param name="generatedNumber">Число, загаданное игрой</param>
        /// <param name="userNumber">Число, введенное пользователем</param>
        /// <returns><c>0</c>, если ни одна цифра не совпала; иначе количество совпадений <c>(больше 0)</c></returns>
        public static int GetBullGuesses(long generatedNumber, long userNumber)
        {
            int correctGuessesAmount = 0;
            
            // Цикл для последовательного сравнения цифр числа.
            while (generatedNumber != 0 || userNumber != 0)
            {
                // Получение последней цифры в числах.
                int generatedNumberDigit = (int)(generatedNumber % 10);
                int userNumberDigit = (int)(userNumber % 10);
                
                if (generatedNumberDigit == userNumberDigit)
                {
                    correctGuessesAmount += 1;
                }
                
                generatedNumber /= 10;
                userNumber /= 10;
            }

            return correctGuessesAmount;
        }
        
        /// <summary>
        /// Считает количество одинаковых цифр, при этом не являющихся индексными совпадениями.
        /// </summary>
        /// <param name="generatedNumber">Число, загаданное игрой</param>
        /// <param name="userNumber">Число, введенное пользователем</param>
        /// <returns><c>0</c>, если числа не равны, как строки; иначе количество вхождений <c>(больше 0)</c></returns>
        public static int GetCowGuesses(long generatedNumber, long userNumber)
        {
            int correctGuessesAmount = 0;
            
            // Строковое представление чисел.
            string generatedNumberString = generatedNumber.ToString();
            string userNumberString = userNumber.ToString();
            
            // Внешний цикл для фиксации цифры загаданного числа.
            for (int i = 0; i < generatedNumberString.Length; i++)
            {
                for (int j = 0; j < userNumberString.Length; j++)
                {
                    if (generatedNumberString[i] == userNumberString[j] && i != j)
                    {
                        correctGuessesAmount += 1;
                    }
                }
            }

            return correctGuessesAmount;
        }
    }
}