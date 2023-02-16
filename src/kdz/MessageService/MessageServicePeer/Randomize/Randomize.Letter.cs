using System.Collections.Generic;
using MessageServicePeer.Types;

namespace MessageServicePeer.Randomize
{
    public sealed partial class Randomize
    {
        /// <summary>
        /// Метод, генерирующий случайную тему сообщения.
        /// </summary>
        /// <returns>Случайная тема.</returns>
        public string GetRandomSubject()
        {
            return GetRandomSequence(
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ", 
                GetRandomNumber(24 + 1));
        }

        /// <summary>
        /// Метод, генерирующий случайную содержание сообщения.
        /// </summary>
        /// <returns>Случайное содержание.</returns>
        public string GetRandomMessage()
        {
            return GetRandomSequence(
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789\t\n\r\x0b\x0c ",
                GetRandomNumber(128 + 1));
        }

        /// <summary>
        /// Метод, генерирующий случайное письмо.
        /// </summary>
        /// <returns>Случайное письмо.</returns>
        public Letter GetRandomLetter()
        {
            var subject = GetRandomSubject();
            var message = GetRandomMessage();
            var senderId = GetRandomEmailFromExisting("");
            var receiverId = GetRandomEmailFromExisting(senderId);
            
            return new Letter
            {
                Subject = subject,
                Message = message,
                SenderId = senderId,
                ReceiverId = receiverId
            };
        }

        /// <summary>
        /// Метод, генерирующий список случайных писем.
        /// </summary>
        /// <param name="emails">Пользовательские емейлы.</param>
        /// <returns>Список случайных писем.</returns>
        public List<Letter> GetRandomLetters(List<string> emails)
        {
            _emails = emails;
            var amount = GetRandomNumber(32 + 1);
            
            var letters = new List<Letter>();
            for (int i = 0; i < amount; i++)
            {
                letters.Add(GetRandomLetter());
            }

            return letters;
        }
    }
}