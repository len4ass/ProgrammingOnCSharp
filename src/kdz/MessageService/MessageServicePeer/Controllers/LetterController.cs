using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MessageServicePeer.Interfaces;
using MessageServicePeer.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessageServicePeer.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за сообщения.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LetterController : ControllerBase
    {
        private readonly ILetterRepository _letterRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<LetterController> _logger;

        /// <summary>
        /// Конструктор контроллера для его привязки.
        /// </summary>
        /// <param name="letterRepository">Репозиторий для связки.</param>
        /// <param name="userRepository">Репозиторий для связки.</param>
        /// <param name="logger">Интерфейс для логирования.</param>
        public LetterController(ILetterRepository letterRepository, 
            IUserRepository userRepository,
            ILogger<LetterController> logger)
        {
            _letterRepository = letterRepository;
            _userRepository = userRepository;
            _logger = logger;
        }
        
        /// <summary>
        /// Запрос для заполнения списка сообщений из файла.
        /// </summary>
        /// <returns>Ok, если удалось заполнить список из файла; иначе BadRequest.</returns>
        [HttpPost("fill-letters-from-file")]
        public ActionResult FillFromFile()
        {
            if (!_letterRepository.FillListFromFile())
            {
                return BadRequest("Не удалось заполнить список сообщений: файла не существует.");
            }

            return Ok("Список сообщений заполнен успешно.");
        }
        
        /// <summary>
        /// Запрос для инициализации (генерации) списка писем.
        /// </summary>
        /// <returns>Ok, если количество пользователей >=2; иначе - BadRequest.</returns>
        [HttpPost("initialize")]
        public ActionResult InitializeLetters()
        {
            var userCount = _userRepository.GetUserCount();
            if (userCount < 2)
            {
                return BadRequest("Не удалось сгенерировать письма: недостаточно пользователей.\n" +
                                  "Попробуйте добавить пользователей или сгенерируйте их с помощью программы.");
            }

            _letterRepository.Initialize(_userRepository.GetAllUsers().Select(x => x.Email).ToList());
            return Ok("Список писем сгенерирован успешно.");
        }

        /// <summary>
        /// Запрос для получения списка всех сообщений.
        /// </summary>
        /// <returns>Ok со списком сообщений, если в списке есть сообщения; иначе - NotFound.</returns>
        [HttpGet("get-all-letters")]
        public ActionResult<List<Letter>> GetAllLetters()
        {
            if (_letterRepository.GetLetterCount() == 0)
            {
                return NotFound("Список сообщений пуст.");
            }
            
            return Ok(_letterRepository.GetAllLetters());
        }

        /// <summary>
        /// Запрос для получения списка сообщений по отправителю и получателю.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="receiver">Получатель.</param>
        /// <returns>Ok со списком сообщений, если удалось найти сообщения; иначе - NotFound.</returns>
        [HttpGet("get-letters-by-sender-and-receiver")]
        public ActionResult<List<Letter>> GetLettersBySenderAndReceiver([Required] string sender, [Required] string receiver)
        {
            if (_letterRepository.GetLetterCount() == 0)
            {
                return NotFound("В списке недостаточно сообщений для поиска: он пуст.");
            }

            var foundLetters = _letterRepository.GetLettersBySenderAndReceiver(sender, receiver);
            if (foundLetters.Count == 0)
            {
                return NotFound("По вашему запросу не найдено сообщений.");
            }
            
            return Ok(foundLetters);
        }

        /// <summary>
        /// Запрос для получения списка сообщений по отправителю.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <returns>Ok со списком сообщений, если удалось найти сообщения; иначе - NotFound.</returns>
        [HttpGet("get-letters-by-sender")]
        public ActionResult<List<Letter>> GetLettersBySender([Required] string sender)
        {
            if (_letterRepository.GetLetterCount() == 0)
            {
                return NotFound("В списке недостаточно сообщений для поиска: он пуст.");
            }

            var foundLetters = _letterRepository.GetLettersBySender(sender);
            if (foundLetters.Count == 0)
            {
                return NotFound("По вашему запросу не найдено сообщений.");
            }
            
            return Ok(foundLetters);
        }
        
        /// <summary>
        /// Запрос для получения списка сообщений по получателю.
        /// </summary>
        /// <param name="receiver">Получатель.</param>
        /// <returns>Ok со списком сообщений, если удалось найти сообщения; иначе - NotFound.</returns>
        [HttpGet("get-letters-by-receiver")]
        public ActionResult<List<Letter>> GetLettersByReceiver([Required] string receiver)
        {
            if (_letterRepository.GetLetterCount() == 0)
            {
                return NotFound("В списке недостаточно сообщений для поиска: он пуст.");
            }

            var foundLetters = _letterRepository.GetLettersByReceiver(receiver);
            if (foundLetters.Count == 0)
            {
                return NotFound("По вашему запросу не найдено сообщений.");
            }
            
            return Ok(foundLetters);
        }

        /// <summary>
        /// Запрос для очистки списка сообщений.
        /// </summary>
        /// <returns>Ok при успешной очистке списка.</returns>
        [HttpPost("clear-letters")]
        public ActionResult ClearLetters()
        {
            _letterRepository.ClearLetters();
            return Ok("Успешно очищен список писем.");
        }

        /// <summary>
        /// Запрос для добавления сообщения в список сообщений.
        /// </summary>
        /// <param name="letter">Сообщение для добавления.</param>
        /// <returns>Ok, если успешно; иначе BadRequest</returns>
        [HttpPost("add-letter")]
        public ActionResult AddLetter([FromForm][Required] Letter letter)
        {
            var (existsSender, _) = _userRepository.GetUserByEmail(letter.SenderId);
            var (existsReceiver, _) = _userRepository.GetUserByEmail(letter.ReceiverId);

            if (existsSender && existsReceiver)
            {
                _letterRepository.AddLetter(letter);
                return Ok("Сообщение успешно добавлено в список.");
            }

            return BadRequest("Не удалось добавить сообщение в список: отправителя или получателя не существует.");
        }
    }
}