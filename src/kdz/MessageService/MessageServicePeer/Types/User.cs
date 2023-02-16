using System;
using System.Text.Json.Serialization;

namespace MessageServicePeer.Types
{
    /// <summary>
    /// Класс, хранящий данные о пользователе.
    /// </summary>
    [Serializable]
    public sealed class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        
        /// <summary>
        /// Email адрес пользователя.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}