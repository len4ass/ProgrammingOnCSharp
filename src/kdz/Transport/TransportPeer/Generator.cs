using System;
using System.Linq;
using EKRLib;

namespace TransportPeer
{
    /// <summary>
    /// Класс для генерации траспортных средств.
    /// </summary>
    internal sealed class Generator
    {
        /// <summary>
        /// Всемогущий рандом.
        /// </summary>
        private readonly Random _random = new();

        /// <summary>
        /// Строка допустимых символов для генерации имени модели.
        /// </summary>
        private const string AllowedCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Генерация массива.
        /// </summary>
        /// <param name="minimumValue">Минимальный размер массива.</param>
        /// <param name="maximumValue">Максимальный размер массива.</param>
        /// <returns>Массив типа <see cref="Transport"/> с пустыми ссылками размером в диапазоне [minimumValue, maximumValue).</returns>
        private Transport[] GenerateObjectArray(int minimumValue, int maximumValue)
        {
            int elementAmount = _random.Next(minimumValue, maximumValue);
            return new Transport[elementAmount];
        }

        /// <summary>
        /// Генерация модели.
        /// </summary>
        /// <param name="chars">Символы для генерации.</param>
        /// <param name="length">Длина имени модели.</param>
        /// <returns>Строка длины length, состоящая из случайных символов из строки chars.</returns>
        private string GenerateTransportModel(string chars, int length)
        {
            return new string(Enumerable
                .Repeat(chars, length)
                .Select(currentString => currentString[_random.Next(currentString.Length)])
                .ToArray());
        }

        /// <summary>
        /// Генерация мощности.
        /// </summary>
        /// <param name="minimumValue">Минимальное число.</param>
        /// <param name="maximumValue">Максимальное число.</param>
        /// <returns>Беззнаковое число в диапазоне [minimumValue, maximumValue).</returns>
        private uint GenerateTransportPower(int minimumValue, int maximumValue)
        {
            return (uint)_random.Next(minimumValue, maximumValue);
        }
        
        /// <summary>
        /// Генерация транспорта.
        /// </summary>
        /// <returns>Ссылку на объект типа <see cref="Car"/> или <see cref="MotorBoat"/>.</returns>
        private Transport GenerateRandomObject()
        {
            // Равновероятный выбор типа транспорта.
            int randomValue = _random.Next(0, 2);
            if (randomValue == 0)
            {
                // Ограничения по условию:
                // состоит из символов из строки AllowedCharacters, длина имени модели - 5, сила - [10, 100).
                return new Car(GenerateTransportModel(AllowedCharacters,5), GenerateTransportPower(10, 100));
            }

            return new MotorBoat(GenerateTransportModel(AllowedCharacters, 5), GenerateTransportPower(10, 100));
        }

        /// <summary>
        /// Генерирует массив транспорта.
        /// </summary>
        /// <returns>Заполненный массив типа <see cref="Transport"/>.</returns>
        public Transport[] GetRandomTransport()
        {
            // Ограничение по условию на размер массива - [6, 10).
            var allTransport = GenerateObjectArray(6, 10);
            for (int i = 0; i < allTransport.Length; i++)
            {
                while (true)
                {
                    try
                    {
                        allTransport[i] = GenerateRandomObject();
                        Console.WriteLine(allTransport[i].StartEngine());
                        
                        break;
                    }
                    catch (TransportException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            return allTransport;
        }
    }
}