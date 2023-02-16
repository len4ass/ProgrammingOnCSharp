using System.Collections.Generic;
using MessageServicePeer.Types;

namespace MessageServicePeer.Interfaces
{
    /// <summary>
    /// Интерфейс-контракт для LetterRepository.
    /// </summary>
    public interface ILetterRepository
    {
        /// <summary>
        /// Очистка списка сообщений.
        /// </summary>
        void ClearLetters();

        /// <summary>
        /// Заполняет списки данными из файла.
        /// </summary>
        /// <returns><c>true</c>, если удалось заполнить списки; иначе <c>false</c>.</returns>
        bool FillListFromFile();
        
        /// <summary>
        /// Метод инициализиции.
        /// <param name="additionalLetters">Список пользовательских емейлов.</param>
        /// </summary>
        void Initialize(List<string> additionalLetters);

        /// <summary>
        /// Получает количество сообщений.
        /// </summary>
        /// <returns>Количество сообщений.</returns>
        int GetLetterCount();

        /// <summary>
        /// Получает список всех сообщений.
        /// </summary>
        /// <returns>Список всех сообщений.</returns>
        List<Letter> GetAllLetters();
        
        /// <summary>
        /// Получает список сообщений по отправителю и получателю.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="receiver">Получатель.</param>
        /// <returns>Список сообщений, соответствующих условию.</returns>
        List<Letter> GetLettersBySenderAndReceiver(string sender, string receiver);

        /// <summary>
        /// Получает список сообщений по отправителю.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <returns>Список сообщений, соответствующих условию.</returns>
        List<Letter> GetLettersBySender(string sender);

        /// <summary>
        /// Получает список сообщений по получателю.
        /// </summary>
        /// <param name="receiver">Получатель.</param>
        /// <returns>Список сообщений, соответствующих условию.</returns>
        List<Letter> GetLettersByReceiver(string receiver);

        /// <summary>
        /// Добавляет сообщение в список.
        /// </summary>
        /// <param name="letter">Письмо для добавления.</param>
        void AddLetter(Letter letter);
    }
}