using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace NotepadPlus.Workers
{
    /// <summary>
    /// Класс для сериализации объектов.
    /// </summary>
    public class JsonWorker
    {
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private readonly string _path;
        
        /// <summary>
        /// Конструктор сериализатора
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        public JsonWorker(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Десериализация объектов из файла.
        /// </summary>
        /// <typeparam name="T">Тип объекта для десериализации.</typeparam>
        /// <returns>Список объектов, если сериализация удалась, иначе null.</returns>
        public List<T>? CollectJson<T>()
        {
            try
            {
                using var read = new StreamReader(_path);
                string buffer = read.ReadToEnd();
                read.Close();

                var converter = JsonConvert.DeserializeObject<List<T>>(buffer);
                return converter;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Не удалось получить десериализованные объекты:{Environment.NewLine}{e.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }
        
        /// <summary>
        /// Добавляет новый объект в файл.
        /// </summary>
        /// <param name="addObject">Объект для добавления.</param>
        /// <typeparam name="T">Тип добавляемого объекта.</typeparam>
        public void UpdateJson<T>(dynamic addObject)
        {
            try
            {
                using var read = new StreamReader(_path);
                var buffer = read.ReadToEnd();
                read.Close();
                
                // Десериализация.
                var converter = JsonConvert.DeserializeObject<List<T>>(buffer);
                converter ??= new List<T>();
                
                // Если объект уже есть в файле, то не добавляем его.
                if (converter.Cast<dynamic?>()
                    .Where(element => element is not null)
                    .Any(element => element?.Name == addObject.Name))
                {
                    return;
                }
                
                converter.Add(addObject);
                var toWrite = JsonConvert.SerializeObject(converter, Formatting.Indented);
                
                using var sw = new StreamWriter(_path);
                sw.WriteLine(toWrite);
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Не удалось добавить сериализованный объект:{Environment.NewLine}{e.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаление объекта из файла.
        /// </summary>
        /// <param name="removeObject">Объект для удаления.</param>
        /// <typeparam name="T">Тип объекта.</typeparam>
        public void RemoveObjectJson<T>(dynamic removeObject)
        {
            try
            {
                using var read = new StreamReader(_path);
                var buffer = read.ReadToEnd();
                read.Close();

                var converter = JsonConvert.DeserializeObject<List<T>>(buffer);
                if (converter is null)
                {
                    return;
                }
                
                // Поиск и удаление нужного объекта.
                for (int i = converter.Count - 1; i >= 0; i--)
                {
                    if (converter.Cast<dynamic>().ToList()[i].Name == removeObject.Name)
                    {
                        converter.RemoveAt(i);
                    }
                }

                // Перезапись с изменениями.
                var toWrite = JsonConvert.SerializeObject(converter, Formatting.Indented);
                using var write = new StreamWriter(_path);
                write.WriteLine(toWrite);
                write.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Не удалось удалить сериализованный объект:{Environment.NewLine}{e.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}