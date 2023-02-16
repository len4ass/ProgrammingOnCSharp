using System;
using System.Text.Json.Serialization;

namespace MessageServicePeer.Types
{
    /// <summary>
    /// Класс, хранящий данные о сообщении.
    /// </summary>
    [Serializable]
    public sealed class Letter
    {
        /// <summary>
        /// Тема сообщения.
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        
        /// <summary>
        /// Содержимое сообщения.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
        
        /// <summary>
        /// Id отправителя.
        /// </summary>
        [JsonPropertyName("sender_id")]
        public string SenderId { get; set; }
        
        /// <summary>
        /// Id получателя.
        /// </summary>
        [JsonPropertyName("receiver_id")]
        public string ReceiverId { get; set; }
    }
}