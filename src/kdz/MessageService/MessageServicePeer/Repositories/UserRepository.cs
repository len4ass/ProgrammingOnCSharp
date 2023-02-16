using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessageServicePeer.Interfaces;
using MessageServicePeer.Types;
using MessageServicePeer.Workers;

namespace MessageServicePeer.Repositories
{
    /// <summary>
    /// Репозиторий с методами для обработки данных по пользователям.
    /// </summary>
    public sealed class UserRepository : IUserRepository
    {
        private readonly string _jsonPath = "../users.json";
        private List<User> _users = new();
        
        /// <summary>
        /// Сортирует пользователей в лексикографическом порядке по Email.
        /// </summary>
        private void OrderList()
        {
            _users = _users.OrderBy(x => x.Email).ToList();
        }
        
        /// <summary>
        /// Обновляет содержимое файла-хранилища.
        /// </summary>
        private void UpdateFile()
        {
            var serializer = new JsonWorker<List<User>>();
            var json = serializer.Serialize(_users);
            FileWorker.WriteFile(_jsonPath, json);
        }

        /// <summary>
        /// Заполняет списки данными из файла.
        /// </summary>
        /// <returns><c>true</c>, если удалось заполнить списки; иначе <c>false</c>.</returns>
        public bool FillListFromFile()
        {
            if (!File.Exists(_jsonPath))
            {
                return false;
            }
                
            var serializer = new JsonWorker<List<User>>();
            var json = FileWorker.ReadFile(_jsonPath);
            
            _users = serializer.Deserialize(json) ?? new List<User>();
            OrderList();
            
            return true;
        }

        /// <summary>
        /// Очистка списка пользователей.
        /// </summary>
        public void ClearUsers()
        {
            _users.Clear();
            UpdateFile();
        }
        
        /// <summary>
        /// Инициализирует список пользователей.
        /// </summary>
        public void Initialize()
        {
            _users = Randomize.Randomize.Instance.GetRandomUsers();
            
            OrderList();
            UpdateFile();
        }

        /// <summary>
        /// Метод для получения количества пользователей.
        /// </summary>
        /// <returns>Количество пользователей в списке.</returns>
        public int GetUserCount()
        {
            return _users.Count;
        }

        /// <summary>
        /// Метод для поиска пользователя в списке пользователей по Email.
        /// </summary>
        /// <param name="email">Строка Email.</param>
        /// <returns>Кортеж: на первом месте булевое значение - найден такой пользователь или нет;
        /// на втором - пользователь или null, если пользователь по Email не найден.</returns>
        public (bool, User) GetUserByEmail(string email)
        {
            foreach (var element in _users)
            {
                if (email == element.Email)
                {
                    return (true, element);
                }
            }

            return (false, default);
        }

        /// <summary>
        /// Метод для поиска пользователя в списке пользователей по имени пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Кортеж: на первом месте булевое значение - найден такой пользователь или нет;
        /// на втором - пользователь или null, если пользователь по Email не найден.</returns>
        public (bool, User) GetUserByUserName(string userName)
        {
            foreach (var element in _users)
            {
                if (userName == element.UserName)
                {
                    return (true, element);
                }
            }

            return (false, default);
        }

        /// <summary>
        /// Получает limit количество пользователей с offset позиции в списке.
        /// </summary>
        /// <param name="limit">Необходимое количество пользователей</param>
        /// <param name="offset">Сдвиг в списке.</param>
        /// <returns>Список пользователей, удовлетворяющих условию.</returns>
        public List<User> GetUsersByLimitAndOffset(int limit, int offset)
        {
            return _users.Skip(offset).Take(limit).ToList();
        }

        /// <summary>
        /// Метод для получения всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        public List<User> GetAllUsers()
        {
            return _users;
        }
        
        /// <summary>
        /// Метод для добавления пользователя в список всех пользователей.
        /// </summary>
        /// <param name="user">Пользователь для добавления.</param>
        /// <param name="resultType">Передает по ссылке результат добавления.</param>
        /// <returns><c>true</c>, если удалось добавить пользователя в список; иначе - <c>false</c>.</returns>
        public bool AddUser(User user, out string resultType)
        {
            if (_users.Any(element => user.Email == element.Email))
            {
                resultType = "пользователь уже существует";
                return false;
            }

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName))
            {
                resultType = "имя пользователя или Email не могут быть пустыми";
                return false;
            }
            
            _users.Add(user);
            OrderList();
            UpdateFile();

            resultType = $"Пользователь \"{user.Email}\" добавлен в список успешно.";
            return true;
        }
    }
}