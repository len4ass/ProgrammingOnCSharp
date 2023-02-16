using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MessageServicePeer.Interfaces;
using MessageServicePeer.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessageServicePeer.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за пользователей.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILetterRepository _letterRepository;
        private readonly ILogger<UserController> _logger;
        
        /// <summary>
        /// Конструктор контроллера для его привязки.
        /// </summary>
        /// <param name="userRepository">Репозиторий для связки.</param>
        /// <param name="letterRepository">Репозиторий для связки.</param>
        /// <param name="logger">Интерфейс для логирования.</param>
        public UserController(IUserRepository userRepository, 
            ILetterRepository letterRepository,
            ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _letterRepository = letterRepository;
            _logger = logger;
        }

        /// <summary>
        /// Запрос для заполнения списка пользователей из файла.
        /// </summary>
        /// <returns>Ok, если удалось заполнить из файла; иначе BadRequest.</returns>
        [HttpPost("fill-users-from-file")]
        public ActionResult FillFromFile()
        {
            if (!_userRepository.FillListFromFile())
            {
                return BadRequest("Не удалось заполнить список пользователей: файла не существует.");
            }

            return Ok("Список пользователей заполнен успешно.");
        }
        
        /// <summary>
        /// Запрос для инициализации (генерации) списка пользователей.
        /// </summary>
        [HttpPost("initialize")]
        public ActionResult InitializeUsers()
        {
            _userRepository.Initialize();
            _letterRepository.ClearLetters();
            Randomize.Randomize.Instance.ClearEmails();
            return Ok("Список пользователей сгенерирован успешно.");
        }

        /// <summary>
        /// Запрос для получения пользователя по Email.
        /// </summary>
        /// <param name="email">Строка Email.</param>
        /// <returns>Ok с информацией о пользователе, если пользователь найден; иначе - NotFound.</returns>
        [HttpGet("get-user-by-email")]
        public ActionResult<User> GetUserByEmail([Required] string email)
        {
            if (_userRepository.GetUserCount() == 0)
            {
                return NotFound("Список пользователей пуст.");
            }
            
            var (exists, user) = _userRepository.GetUserByEmail(email);
            if (!exists)
            {
                return NotFound($"Пользователя {email} не существует.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Запрос для получения первого пользователя в списке с таким ником.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <returns>Ok с информацией о пользователе, если пользователь найден; иначе - NotFound.</returns>
        [HttpGet("get-user-by-username")]
        public ActionResult<User> GetUsersByUserName([Required] string username)
        {
            if (_userRepository.GetUserCount() == 0)
            {
                return NotFound("Список пользователей пуст.");
            }
            
            var (exists, user) = _userRepository.GetUserByUserName(username);
            if (!exists)
            {
                return NotFound($"Пользователя {username} не существует.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Запрос для получения всех пользователей.
        /// </summary>
        /// <returns>Ok со списком пользователей, если найдены пользователи; иначе NotFound.</returns>
        [HttpGet("get-all-users")]
        public ActionResult<List<User>> GetAllUsers()
        {
            if (_userRepository.GetUserCount() == 0)
            {
                return NotFound("Список пользователей пуст.");
            }
            
            return Ok(_userRepository.GetAllUsers());
        }

        /// <summary>
        /// Запрос для получения limit количества пользователей с offset позиции в списке.
        /// </summary>
        /// <param name="limit">Необходимое количество пользователей.</param>
        /// <param name="offset">Сдвиг в списке.</param>
        /// <returns>Ok со списком пользователей, если данные корректны и найдены пользователи.
        /// NotFound, если по запросу не найдено пользователей. Иначе - BadRequest.</returns>
        [HttpGet("get-users-by-limit-and-offset")]
        public ActionResult<List<User>> GetUsersByLimitAndOffset([Required] int limit, [Required] int offset)
        {
            if (limit <= 0 || offset < 0)
            {
                return BadRequest("Некорректные данные для получения списка пользователей.");
            }
            
            if (_userRepository.GetUserCount() == 0)
            {
                return NotFound("Список пользователей пуст.");
            }

            var foundUsers = _userRepository.GetUsersByLimitAndOffset(limit, offset);
            if (foundUsers.Count == 0)
            {
                return NotFound("По вашему запросу не найдено пользователей.");
            }

            return Ok(foundUsers);
        }

        /// <summary>
        /// Запрос для очистки списка пользователей.
        /// </summary>
        /// <returns>Ok при успешной очистке списка.</returns>
        [HttpPost("clear-users")]
        public ActionResult ClearUsers()
        {
            _userRepository.ClearUsers();
            return Ok("Успешно очищен список пользователей.");
        }
        
        /// <summary>
        /// Запрос для добавления пользователя в список пользователей.
        /// </summary>
        /// <param name="user">Пользователь для добавления.</param>
        /// <returns>Ok, если удалось добавить пользователя в список; иначе - BadRequest.</returns>
        [HttpPost("add-user")]
        public ActionResult AddUser([FromForm][Required] User user)
        {
            bool added = _userRepository.AddUser(user, out string result);
            if (!added)
            {
                return BadRequest($"Не удалось добавить пользователя \"{user.Email}\" в список: {result}.");
            }

            return Ok($"{result}");
        }
    }
}