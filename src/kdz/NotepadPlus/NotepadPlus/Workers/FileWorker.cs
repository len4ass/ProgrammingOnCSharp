using System;
using System.IO;
using System.Windows.Forms;

namespace NotepadPlus.Workers
{
    /// <summary>
    /// Класс для работы с файлами.
    /// </summary>
    public class FileWorker
    {
        /// <summary>
        /// Потоковая запись в файл.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="text">Текст для записи.</param>
        public static void WriteFile(string path, string? text)
        {
            try
            {
                using var writeStream = new StreamWriter(path);
                writeStream.Write(text);
                writeStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Не удалось записать текст в файл:{Environment.NewLine}{e.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Потоковое чтение файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Строку, если чтение удалось, иначе null.</returns>
        public static string? ReadFile(string path)
        {
            try
            {
                using var readStream = new StreamReader(path);
                string buffer = readStream.ReadToEnd();
                readStream.Close();

                return buffer;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Не удалось прочитать текст из файла:{Environment.NewLine}{e.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }
    }
}