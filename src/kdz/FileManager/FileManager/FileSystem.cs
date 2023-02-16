using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс для работы с файловой системой.
    /// </summary>
    public class FileSystem
    {
        private readonly string _path;

        /// <summary>
        /// Конструктор для создания экземпляра класса с путем к файлу.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        public FileSystem(string path)
        {
            try
            {
                _path = path;
            }
            catch (Exception)
            {
                Console.WriteLine("\nНе удалось инициализировать класс для работы с файлами.");
            }
        }

        /// <summary>
        /// Получает путь к файлу.
        /// </summary>
        /// <returns>Возвращает строку, содержащую путь к файлу; <c>null</c>, если не удалось получить ее.</returns>
        public string GetFilePath()
        {
            try
            {
                return _path;
            }
            catch (Exception)
            {
                Console.WriteLine("\nНе удалось получить путь к файлу.");
                return null;
            }
        }

        /// <summary>
        /// Получает список дисков компьютера.
        /// </summary>
        /// <returns>Возвращает массив строк с дисками; <c>null</c>, если не удалось получить его.</returns>
        public static string[] GetDisks()
        {
            try
            {
                var drivesInfo = DriveInfo.GetDrives();
                var drives = new List<string>();

                foreach (var drive in drivesInfo)
                {
                    drives.Add(drive.Name);
                }

                return drives.ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("\nНедостаточно прав для получения информации о дисках, попробуйте заново.");
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("\nПроизошла непредвиденная ошибка при попытке получения информации о дисках, " +
                                  "попробуйте заново.");
                return null;
            }
        }

        /// <summary>
        /// Проверяет на корректность путь.
        /// </summary>
        /// <param name="directoryName">Путь к директории.</param>
        /// <returns><c>true</c>, если путь корректен; иначе <c>false</c>.</returns>
        public static bool ValidateDirectory(ref string directoryName)
        {
            try
            {
                directoryName = Path.GetFullPath(directoryName);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("\nВведенный путь имеет некорректный вид, попробуйте заново.");
                return false;
            }
        }
        
        /// <summary>
        /// Проверяет директорию на существование.
        /// </summary>
        /// <param name="directoryName">Путь к директории.</param>
        /// <returns><c>true</c>, если директория существует; иначе <c>false</c>.</returns>
        public static bool DirectoryExists(ref string directoryName)
        {
            try
            {
                if (ValidateDirectory(ref directoryName))
                {
                    if (Path.IsPathRooted(directoryName))
                    {
                        return Directory.Exists(directoryName);
                    }
                }

                return false;
            }
            catch (Exception)
            {
                Console.WriteLine("\nВведенный путь некорректен, попробуйте заново.");
                return false;
            }
        }

        /// <summary>
        /// Получает текущую директорию.
        /// </summary>
        /// <returns>Возвращает строку текущей директории; <c>null</c>, если не удалось получить ее.</returns>
        public static string GetCurrentDirectory()
        {
            try
            {
                return Directory.GetCurrentDirectory();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("\nНедостаточно прав для получения текущей директории, попробуйте заново.");
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("\nПроизошла непредвиденная ошибка при попытке получения текущей директории, " +
                                  "попробуйте заново.");
                return null;
            }
        }

        /// <summary>
        /// Устанавливает текущую директорию.
        /// </summary>
        /// <param name="folderName">Путь к директории.</param>
        /// <returns><c>true</c>, если удалось установить директорию; иначе <c>false</c>.</returns>
        public static bool SetCurrentDirectory(string folderName)
        {
            try
            {
                Directory.SetCurrentDirectory(folderName);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось перейти в директорию {folderName}, попробуйте заново.");
                return false;
            }
        }

        /// <summary>
        /// Создает новую директорию.
        /// </summary>
        /// <param name="path">Путь к новой директории.</param>
        /// <param name="newPath">Передает по ссылке путь к созданной директории.</param>
        /// <returns><c>true</c>, если удалось создать директорию; иначе <c>false</c>.</returns>
        public static bool CreateDirectory(string path, out string newPath)
        {
            try
            {
                newPath = path;
                if (Directory.Exists(path))
                {
                    Console.WriteLine("\nТакая директория уже существует.");
                    newPath = Path.GetFullPath(path);
                    return true;
                }
                
                var info = Directory.CreateDirectory(path);
                newPath = info.FullName;
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("\nНе удалось создать такую директорию, попробуйте заново.");
                newPath = path;
                return false;
            }
        }

        /// <summary>
        /// Получает список поддиректорий.
        /// </summary>
        /// <param name="path">Путь к текущей директории</param>
        /// <returns>Возвращает массив строк с поддиректориями; <c>null</c>, если не удалось получить список.</returns>
        public static string[] GetSubDirectories(string path)
        {
            try
            {
                var directories = Directory.GetDirectories(path).ToList();
                directories.Add("Подняться вверх на одну директорию");
                directories.Add("Сохранить текущую директорию и вернуться в главное меню");

                return directories.ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("\nНедостаточно прав для получения информации о директории, попробуйте заново.");
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("\nНе удалось найти поддиректории, попробуйте заново.");
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("\nПроизошла непредвиденная ошибка при попытке получения информации о директории, " +
                                  "попробуйте заново.");
                return null;
            }
        }

        /// <summary>
        /// Получает список файлов в директории.
        /// </summary>
        /// <param name="path">Путь к директории.</param>
        /// <param name="mask">Опциональный параметр - маска для поиска файлов.</param>
        /// <param name="allDirs">Опциональный параметр - <c>true</c> - поиск по всем поддиректориям; <c>false</c> -
        /// поиск в текущей директории. </param>
        /// <returns></returns>
        public static string[] GetFilesInDirectory(string path, string mask = null, bool allDirs = false)
        {
            try
            {
                List<string> files;
                
                // Сканирование всех поддиректорий по маске.
                if (allDirs)
                {
                    files = Directory.GetFiles(path, mask ?? "", SearchOption.AllDirectories).
                        ToList();
                    files.Add("Вернуться в главное меню");
                    return files.ToArray();
                }
                
                // Сканирование по маске
                if (string.IsNullOrEmpty(mask))
                {
                    files = Directory.GetFiles(path).ToList();
                    files.Add("Вернуться в главное меню");
                    return files.ToArray();
                }

                files = Directory.GetFiles(path, mask).ToList();
                files.Add("Вернуться в главное меню");
                return files.ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("\nНедостаточно прав для получения информации о директории, попробуйте заново.");
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("\nНе удалось найти эту директорию, попробуйте заново.");
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("\nПроизошла непредвиденная ошибка при попытке получения информации о директории, " +
                                  "попробуйте заново.");
                return null;
            }
        }

        /// <summary>
        /// Получает имя файла.
        /// </summary>
        /// <returns>Строка с именем файла.</returns>
        public string GetFileName()
        {
            try
            {
                return Path.GetFileName(_path);
            }
            catch (Exception)
            {
                Console.WriteLine("\nНе удалось получить имя файла, попробуйте заново.");
                return null;
            }
        }
        
        /// <summary>
        /// Получает расширение файла.
        /// </summary>
        /// <returns>Строка с расширением.</returns>
        public string GetFileExtension()
        {
            try
            {
                return Path.GetExtension(_path);
            }
            catch (Exception)
            {
                Console.WriteLine("\nНе удалось получить расширение файла, попробуйте заново.");
                return null;
            }
        }

        /// <summary>
        /// Читает файл текущего экземпляра класса.
        /// </summary>
        /// <param name="encoding">Кодировка для чтения файла.</param>
        /// <returns><c>true</c>, если удалось прочитать файл; иначе <c>false</c>.</returns>
        public bool ReadTextFile(Encoding encoding)
        {
            try
            {
                using var streamRead = new StreamReader(_path, encoding, false);
                Console.WriteLine($"\nСодержимое файла {GetFileName()}: \n");
                Console.WriteLine(streamRead.ReadToEnd());
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось прочитать файл {GetFileName()}, попробуйте заново.");
                return false;
            }
        }

        /// <summary>
        /// Читает файл в заданной кодировке.
        /// </summary>
        /// <param name="encoding">Кодировка для чтения файла.</param>
        /// <param name="buffer">По ссылке передает строку с содержимым файла.</param>
        /// <returns><c>true</c>, если удалось прочитать файл; иначе <c>false</c>.</returns>
        public bool ReadTextFile(Encoding encoding, out string buffer)
        {
            try
            {
                using var streamRead = new StreamReader(_path, encoding, false);
                buffer = streamRead.ReadToEnd();
                Console.WriteLine($"\nСодержимое файла {GetFileName()}: " +
                                  $"\n{buffer}");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось прочитать файл {GetFileName()}, попробуйте заново.");
                buffer = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Создает файл в заданной кодировке.
        /// </summary>
        /// <param name="text">Текст для записи в файл.</param>
        /// <param name="encoding">Кодировка для создания файла.</param>
        /// <returns><c>true</c>, если удалось создать файл; иначе <c>false</c>.</returns>
        public bool CreateTextFile(string text, Encoding encoding)
        {
            try
            {
                if (File.Exists(_path))
                { 
                    Console.WriteLine($"\nФайл {GetFileName()} уже существует, попробуйте заново.");
                    return false;
                }

                File.WriteAllText(_path, text, encoding);
                Console.WriteLine($"\nФайл {GetFileName()} успешно создан.");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось создать файл {GetFileName()}, попробуйте заново.");
                return false;
            }
        }

        /// <summary>
        /// Перемещает файл в указанную директорию.
        /// </summary>
        /// <param name="path">Директория, в которую будет перемещен файл.</param>
        /// <returns><c>true</c>, если удалось переместить файл; иначе <c>false</c>.</returns>
        public bool MoveFile(string path)
        {
            try
            {
                string newPath = Path.Combine(path, GetFileName());
                if (!File.Exists(_path))
                {
                    Console.WriteLine($"\nФайл для перемещения {GetFileName()} не существует, попробуйте заново.");
                    return false;
                }

                if (_path == newPath)
                {
                    Console.WriteLine("\nПеремещение файла в исходную директорию не предусмотрено, попробуйте заново.");
                    return false;
                }

                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                }
                
                File.Move(_path, newPath);
                if (File.Exists(newPath) && !File.Exists(_path))
                {
                    Console.WriteLine($"\nФайл {GetFileName()} перемещен в {path}.");
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось переместить файл {GetFileName()} в {path}, попробуйте заново.");
                return false;
            }
        }

        /// <summary>
        /// Копирует файл в указанную директорию.
        /// </summary>
        /// <param name="path">Директория, в которую будет скопирован файл.</param>
        /// <param name="overWrite">Перадает значение, используемое для file overwrite.</param>
        /// <returns><c>true</c>, если удалось скопировать файл; иначе <c>false</c>.</returns>
        public bool CopyFile(string path, bool overWrite)
        {
            try
            {
                string newPath = Path.Combine(path, GetFileName());
                if (!File.Exists(_path))
                {
                    Console.WriteLine($"\nФайл для копирования {GetFileName()} не существует, попробуйте заново.");
                    return false;
                }

                if (_path == newPath)
                {
                    Console.WriteLine($"\nФайл {GetFileName()} скопирован в {path}.");
                    return true;
                }

                if (File.Exists(newPath))
                {
                    if (overWrite)
                    {
                        File.Delete(newPath);
                    }
                    else
                    {
                        Console.WriteLine($"\nФайл {GetFileName()} не был заменен в {path}.");
                        return false;
                    }
                }

                File.Copy(_path, newPath);
                if (File.Exists(_path) && File.Exists(newPath))
                {
                    Console.WriteLine($"\nФайл {GetFileName()} скопирован в {path}.");
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось копировать файл {GetFileName()} в {path}, попробуйте заново.");
                return false;
            }
        }
        
        /// <summary>
        /// Удаляет файл.
        /// </summary>
        /// <returns><c>true</c>, если удалось удалить файл; иначе <c>false</c>.</returns>
        public bool DeleteFile()
        {
            try
            {
                if (!File.Exists(_path))
                {
                    Console.WriteLine($"Файл {GetFileName()} не существует, попробуйте заново.");
                    return false;
                }
                
                File.Delete(_path);
                Console.WriteLine($"\nФайл {GetFileName()} успешно удален.");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"Не удалось удалить файл {GetFileName()}, попробуйте заново.");
                return false;
            }
        }
    }
}