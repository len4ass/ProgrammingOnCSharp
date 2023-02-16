using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessageServicePeer.Interfaces;
using MessageServicePeer.Types;
using MessageServicePeer.Workers;

namespace MessageServicePeer.Repositories
{
    /// <summary>
    /// Репозиторий с методами для обработки данных по сообщениям.
    /// </summary>
    public sealed class LetterRepository : ILetterRepository
    {
        private readonly string _jsonPath = "../letters.json";
        private List<Letter> _letters = new();

        /// <summary>
        /// Обновляет содержимое файла-хранилища.
        /// </summary>
        private void UpdateFile()
        {
            var serializer = new JsonWorker<List<Letter>>();
            var json = serializer.Serialize(_letters);
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
                
            var serializer = new JsonWorker<List<Letter>>();
            var json = FileWorker.ReadFile(_jsonPath);
            
            _letters = serializer.Deserialize(json) ?? new List<Letter>();
            return true;
        }

        /// <summary>
        /// Очистка списка сообщений.
        /// </summary>
        public void ClearLetters()
        {
            _letters.Clear();
        }
        
        /// <summary>
        /// Инициализирует список писем.
        /// </summary>
        public void Initialize(List<string> additionalEmails)
        {
            _letters = Randomize.Randomize.Instance.GetRandomLetters(additionalEmails);

            UpdateFile();
        }

        /// <summary>
        /// Получает количество сообщений.
        /// </summary>
        /// <returns>Количество сообщений.</returns>
        public int GetLetterCount()
        {
            return _letters.Count;
        }
        
        /// <summary>
        /// Получает список всех сообщений.
        /// </summary>
        /// <returns>Список всех сообщений.</returns>
        public List<Letter> GetAllLetters()
        {
            return _letters;
        }
        
        /// <summary>
        /// Получает список сообщений по отправителю и получателю.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="receiver">Получатель.</param>
        /// <returns>Список сообщений, соответствующих условию.</returns>
        public List<Letter> GetLettersBySenderAndReceiver(string sender, string receiver)
        {
            return _letters.Where(x => x.SenderId == sender && x.ReceiverId == receiver).ToList();
        }

        /// <summary>
        /// Получает список сообщений по отправителю.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <returns>Список сообщений, соответствующих условию.</returns>
        public List<Letter> GetLettersBySender(string sender)
        {
            return _letters.Where(x => x.SenderId == sender).ToList();
        }

        /// <summary>
        /// Получает список сообщений по получателю.
        /// </summary>
        /// <param name="receiver">Получатель.</param>
        /// <returns>Список сообщений, соответствующих условию.</returns>
        public List<Letter> GetLettersByReceiver(string receiver)
        {
            return _letters.Where(x => x.ReceiverId == receiver).ToList();
        }

        /// <summary>
        /// Добавляет сообщение в список.
        /// </summary>
        /// <param name="letter">Письмо для добавления.</param>
        public void AddLetter(Letter letter)
        {
            _letters.Add(letter);
            UpdateFile();
        }
    }
}