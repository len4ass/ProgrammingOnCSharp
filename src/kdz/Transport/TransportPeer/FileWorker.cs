using System;
using System.IO;
using System.Text;

namespace TransportPeer
{
    /// <summary>
    /// Класс для работы с файлами
    /// </summary>
    internal static class FileWorker
    {
        /// <summary>
        /// Записывает информацию в файл в кодировке UTF-16 (Unicode).
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="content">Информация для записи.</param>
        public static void WriteFile(string path, string content)
        {
            try
            {
                // Если файл не существует, то он автоматически будет создан и в него будет записан content.
                // Если файл существует, то будет стерто его содержимое и в него будет записан content.
                using var stream = new StreamWriter(path, false, Encoding.Unicode);
                stream.Write(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Не удалось записать информацию в файл.");
            }
        }
    }
}