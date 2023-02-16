using System;
using System.Text.Json;

namespace MessageServicePeer.Workers
{
    /// <summary>
    /// Класс для сериализации и десериализации объектов.
    /// </summary>
    /// <typeparam name="T">Тип.</typeparam>
    public class JsonWorker<T>
    {
        /// <summary>
        /// Метод для сериализации объектов.
        /// </summary>
        /// <param name="obj">Объект, который будет сериализован.</param>
        /// <returns>Строковое (json) представление объекта.</returns>
        public string Serialize(T obj)
        {
            try
            {
                var json = JsonSerializer.Serialize(obj);
                return json;
            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось сериализовать объект.");
                return default;
            }
        }

        /// <summary>
        /// Метод для десериализации объектов.
        /// </summary>
        /// <param name="json">Строковое (json) представление объекта.</param>
        /// <returns>Десериализованный объект.</returns>
        public T Deserialize(string json)
        {
            try
            {
                var obj = JsonSerializer.Deserialize<T>(json);
                return obj;
            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось десериализовать строку в объект.");
                return default;
            }
        }
    }
}