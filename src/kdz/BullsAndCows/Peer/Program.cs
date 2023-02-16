namespace Peer
{
    /// <summary>
    /// Главный класс программы
    /// </summary>
    class Program
    {
        /// <summary>
        /// Вызывает методы других классов для контроля ввода, генерации чисел и ведения игры, а также осуществляет
        /// контроль повторного решения
        /// </summary>
        static void Main()
        {
            while (true)
            {
                Input.ControlNumberLenght(out int numberLenght);
                Input.ControlNumberGeneration(numberLenght, out long numberRandom);
                Input.ControlNumberGuesses(numberLenght, numberRandom);

                if (!Input.ControlRun())
                {
                    break;
                }
            }
        }
    }
}