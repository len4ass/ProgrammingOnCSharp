using System.Collections.Generic;
using MessageServicePeer.Types;

namespace MessageServicePeer.Randomize
{
    public sealed partial class Randomize
    {
        /// <summary>
        /// Метод, генерирующий случайное имя пользователя.
        /// </summary>
        /// <returns>Случайное имя пользователя.</returns>
        public string GetRandomUserName()
        {
            return GetRandomSequence(
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
                16);
        }

        /// <summary>
        /// Метод, генерирующий случайного пользователя.
        /// </summary>
        /// <returns>Случайный пользователь.</returns>
        public User GetRandomUser()
        {
            while (true)
            {
                var userName = GetRandomUserName();
                var email = GetRandomEmail();

                if (_emails.Contains(email))
                {
                    continue;
                }
                
                _emails.Add(email);
                return new User
                {
                    UserName = userName,
                    Email = email
                };
            }
        }
        
        /// <summary>
        /// Метод, генерирующий список случайных пользователей.
        /// </summary>
        /// <returns>Список случайных пользователей.</returns>
        public List<User> GetRandomUsers()
        {
            ClearEmails();
            var amount = GetRandomNumber(32 + 1);
            
            var users = new List<User>();
            for (int i = 0; i < amount; i++)
            {
                users.Add(GetRandomUser());
            }

            return users;
        }
    }
}