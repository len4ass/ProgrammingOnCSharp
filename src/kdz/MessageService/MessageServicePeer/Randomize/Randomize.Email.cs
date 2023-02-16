using System;
using System.Collections.Generic;

namespace MessageServicePeer.Randomize
{
    public sealed partial class Randomize
    {
        private List<string> _emails = new();
        
        /// <summary>
        /// Чистит список емейлов.
        /// </summary>
        public void ClearEmails()
        {
            _emails.Clear();
        }
        
        /// <summary>
        /// Метод, генерирующий случайный Email.
        /// </summary>
        /// <returns>Случайный Email.</returns>
        public string GetRandomEmail()
        {
            return GetRandomSequence(
                "abcdefghijklmnopqrstuvwxyz",
                GetRandomNumber(16)) + "@mail.ru";
        }

        /// <summary>
        /// Возвращает Email, который уже записан в списке пользователей.
        /// </summary>
        /// <param name="avoid">Email, который нужно исключить из списка.</param>
        /// <returns></returns>
        public string GetRandomEmailFromExisting(string avoid)
        {
            while (true)
            {
                var index = _random.Next(0, _emails.Count);
                if (_emails[index] == avoid)
                {
                    continue;
                }

                return _emails[index];
            }
        }
    }
}