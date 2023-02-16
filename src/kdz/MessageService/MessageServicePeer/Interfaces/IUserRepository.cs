using System.Collections.Generic;
using MessageServicePeer.Types;

namespace MessageServicePeer.Interfaces
{
    /// <summary>
    /// Интерфейс-контракт для UserRepository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Очистка списка пользователей.
        /// </summary>
        void ClearUsers();

        /// <summary>
        /// Заполняет списки данными из файла.
        /// </summary>
        /// <returns><c>true</c>, если удалось заполнить списки; иначе <c>false</c>.</returns>
        bool FillListFromFile();
        
        /// <summary>
        /// Метод инициализации.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Метод для получения количества пользователей.
        /// </summary>
        /// <returns>Количество пользователей в списке.</returns>
        int GetUserCount();
        
        /// <summary>
        /// Метод для поиска пользователя по Email.
        /// </summary>
        /// <param name="email">Строка Email.</param>
        /// <returns>Кортеж: на первом месте булевое значение - найден такой пользователь или нет;
        /// на втором - пользователь или null, если пользователь по Email не найден.</returns>
        (bool, User) GetUserByEmail(string email);

        /// <summary>
        /// Метод для поиска пользователя в списке пользователей по имени пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Кортеж: на первом месте булевое значение - найден такой пользователь или нет;
        /// на втором - пользователь или null, если пользователь по Email не найден.</returns>
        (bool, User) GetUserByUserName(string userName);
        
        /// <summary>
        /// Получает limit количество пользователей с offset позиции в списке.
        /// </summary>
        /// <param name="limit">Необходимое количество пользователей</param>
        /// <param name="offset">Сдвиг в списке.</param>
        /// <returns>Список пользователей, удовлетворяющих условию.</returns>
        List<User> GetUsersByLimitAndOffset(int limit, int offset);
        
        /// <summary>
        /// Метод для получения всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        List<User> GetAllUsers();

        /// <summary>
        /// Метод для добавления пользователя в список всех пользователей.
        /// </summary>
        /// <param name="user">Пользователь для добавления.</param>
        /// <param name="resultType">Передает по ссылке результат добавления.</param>
        /// <returns><c>true</c>, если удалось добавить пользователя в список; иначе - <c>false</c>.</returns>
        bool AddUser(User user, out string resultType);
    }
}