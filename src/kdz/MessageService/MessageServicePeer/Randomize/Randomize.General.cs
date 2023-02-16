using System.Linq;

namespace MessageServicePeer.Randomize
{
    public sealed partial class Randomize
    {
        /// <summary>
        /// Метод, генерирующий случайное число.
        /// </summary>
        /// <returns>Случайное число диапазона Int32.</returns>
        public int GetRandomNumber()
        {
            return _random.Next();
        }

        /// <summary>
        /// Метод, генерирующий случайное число с ограничением сверху.
        /// </summary>
        /// <param name="upperLimit">Ограничение сверху.</param>
        /// <returns>Случайное число Int32, ограниченное сверху.</returns>
        public int GetRandomNumber(int upperLimit)
        {
            return _random.Next(2, upperLimit);
        }
        
        /// <summary>
        /// Метод, генерирующий случайную последовательность символов.
        /// </summary>
        /// <param name="characters">Допустимые символы в последовательности.</param>
        /// <param name="length">Длина выходной последовательность.</param>
        /// <returns>Случайная последовательность из указанных символов, указанной длины.</returns>
        public string GetRandomSequence(string characters, int length)
        {
            return new string(Enumerable
                .Repeat(characters, length)
                .Select(currentString => currentString[_random.Next(currentString.Length)])
                .ToArray());
        }
    }
}