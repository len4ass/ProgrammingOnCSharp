using System;
using System.IO;

namespace MessageServicePeer.Workers
{
    /// <summary>
    /// Класс для работы с файлами.
    /// </summary>
    public static class FileWorker
    {
        /// <summary>
        /// Метод для записи в файл.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="buffer">Контент для записи.</param>
        public static void WriteFile(string path, string buffer)
        {
            try
            {
                using var writer = new StreamWriter(path);
                writer.Write(buffer);
            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось произвести запись в файл.");
            }
        }

        /// <summary>
        /// Метод для чтения файлов
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Контент для записи.</returns>
        public static string ReadFile(string path)
        {
            try
            {
                using var reader = new StreamReader(path);
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось произвести чтение файла.");
                return null;
            }
        }
    }
}