using System;
using System.Runtime.Serialization;

namespace EKRLib
{
    /// <summary>
    /// Класс наследник Exception.
    /// </summary>
    [Serializable]
    public class TransportException : Exception
    {
        /// <summary>
        /// Пустой конструктор (вызывает base()).
        /// </summary>
        public TransportException()
        {
        }
        
        /// <summary>
        /// Генератор исключений типа TransportException с сообщением.
        /// </summary>
        /// <param name="message">Сообщение для исключения.</param>
        public TransportException(string message) : base(message)
        {
        }

        /// <summary>
        /// Генератор исключений типа TransportException с сообщением и пробросом inner.
        /// </summary>
        /// <param name="message">Сообщение для исключения.</param>
        /// <param name="inner">Изначальное исключение.</param>
        public TransportException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Исключение с сериализацией и потоком.
        /// </summary>
        /// <param name="info">Параметры сериализации.</param>
        /// <param name="context">Поток.</param>
        protected TransportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}