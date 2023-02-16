using System;
using System.IO;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс для работы с консолью.
    /// </summary>
    public class Input
    {
        private readonly string[] _operations =
        {
            "Выбрать диск для работы", "Переместиться в другую директорию",
            "Просмотреть список файлов в текущей директории", "Просмотреть список файлов в текущей директории по маске",
            "Просмотреть список файлов в текущей директории и ее поддиректориях по маске",
            "Прочитать текстовый файл в текущей директории", "Создать текстовый файл в текущей директории", 
            "Скопировать файл", "Переместить файл в другую директорию", "Удалить файл", 
            "Конкатенация содержимого нескольких файлов", 
            "Скопировать все файлы из текущей директории и всех её поддиректорий по маске в другую директорию",
            "Завершить сессию"
        };

        // 0, 1200, 65001, 20127 в int.
        private readonly string[] _encodings =
        {
            "Default", "Unicode", "UTF-8", "ASCII"
        };

        private readonly Encoding[] _trueEncodings =
        {
            Encoding.Default, Encoding.Unicode, Encoding.UTF8, Encoding.ASCII
        };
        
        private int _currentEncoding;
        private int _currentFile;
        private int _currentTextFile;
        private int _currentOperation;
        private string _currentMask;
        private string _currentDirectory;
        private string[] _currentSubDirectories;
        private string[] _disks;
        private string[] _files;
        private string[] _maskFiles;
        private string[] _textFiles;
        private string[] _allFiles;

        /// <summary>
        /// Конструктор класса для начала работы.
        /// </summary>
        public Input()
        {
            UpdateCurrentDirectory();
            UpdateDisks();
        }

        /// <summary>
        /// Пустой вызов - заглушка.
        /// </summary>
        private void EmptyCall()
        {
        }

        /// <summary>
        /// Завершение сессии программы.
        /// </summary>
        private void Exit()
        {
            Console.WriteLine("\nЗавершение текущей сессии...");
            Environment.Exit(1);
        }

        /// <summary>
        /// Остановка после выполнения каждого действия для удобства чтения результата.
        /// </summary>
        private void FreezeOutput()
        {
            Console.WriteLine("\nВведите любую последовательность символов, чтобы продолжить.");
            Console.Write("\nВвод: ");
            _ = Console.ReadLine();
        }

        /// <summary>
        /// Обновление поля, хранящего диски.
        /// </summary>
        private void UpdateDisks()
        {
            _disks = FileSystem.GetDisks();
        }
        
        /// <summary>
        /// Обновление поля, хранящего поддиректории.
        /// </summary>
        /// <returns><c>true</c>, если получен корректный массив; иначе <c>false</c>.</returns>
        private bool UpdateCurrentSubDirectories()
        {
            _currentSubDirectories = FileSystem.GetSubDirectories(_currentDirectory);

            if (_currentSubDirectories == null)
            {
                Console.WriteLine("\nНажмите ENTER, чтобы попробовать заново.");
                Console.Write("Ввод: ");
                Console.ReadLine();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Обновление поля, хранящего директории.
        /// </summary>
        /// <returns><c>true</c>, если получен корректный массив; иначе <c>false</c>.</returns>
        private bool UpdateCurrentDirectory()
        {
            _currentDirectory = FileSystem.GetCurrentDirectory();

            if (_currentDirectory == null)
            {
                Console.WriteLine("\nНажмите ENTER, чтобы попробовать заново.");
                Console.Write("Ввод: ");
                Console.ReadLine();
                return false;
            }

            return UpdateCurrentSubDirectories();
        }

        /// <summary>
        /// Обновление поля, хранящего директории, передаваемым значением.
        /// </summary>
        /// <returns><c>true</c>, если получен корректный массив; иначе <c>false</c>.</returns>
        private bool UpdateCurrentDirectory(string path)
        {
            if (!FileSystem.SetCurrentDirectory(path))
            {
                Console.WriteLine("\nНажмите ENTER, чтобы попробовать заново.");
                Console.Write("Ввод: ");
                Console.ReadLine();
                return false;
            }

            return UpdateCurrentDirectory();
        }

        /// <summary>
        /// Вывод в консоль информации из массива.
        /// </summary>
        /// <param name="info">Тип информации.</param>
        /// <param name="array">Массив с информацией</param>
        /// <typeparam name="T">Тип данных массива.</typeparam>
        private void PrintData<T>(string info, in T[] array)
        {
            Console.WriteLine(info);
            if (array is null || array.Length == 0)
            {
                Console.WriteLine();
                Console.Write("Ввод: ");
                return;
            }
            
            // Чтение массива.
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {array[i]}");
                
                if (i == array.Length - 1)
                {
                    Console.WriteLine();
                    Console.Write("Ввод: ");
                }
            }
        }

        /// <summary>
        /// Получение имени файла из консоли.
        /// </summary>
        /// <param name="fileName">По ссылке передает строку с именем файла.</param>
        /// <returns><c>true</c>, если строка валидна; иначе <c>false</c>.</returns>
        private bool GetFileNameFromConsole(out string fileName)
        {
            fileName = Console.ReadLine();
            return !string.IsNullOrEmpty(fileName);
        }

        /// <summary>
        /// Получение имени директории из консоли.
        /// </summary>
        /// <param name="directoryName">По ссылке передает строку с именем директории.</param>
        /// <returns><c>true</c>, если строка валидна; иначе <c>false</c>.</returns>
        private bool GetDirectoryNameFromConsole(out string directoryName)
        {
            directoryName = Console.ReadLine();
            return !string.IsNullOrEmpty(directoryName);
        }

        /// <summary>
        /// Получение текста из консоли.
        /// </summary>
        /// <param name="text">Передает по ссылке строку с текстом.</param>
        private void GetTextFromConsole(out string text)
        {
            text = Console.ReadLine();
            text ??= "";
        }

        /// <summary>
        /// Получает ввод с консоли и валидирует его.
        /// </summary>
        /// <param name="array">Массив с валидным значениями.</param>
        /// <param name="value">Выбранное значение.</param>
        /// <param name="back"><c>true</c>, если пользователь хочет закончить операцию; иначе <c>false</c>.</param>
        /// <returns><c>true</c>, если ввод валиден; иначе <c>false</c>.</returns>
        private bool GetInput(in string[] array, out int value, out bool back)
        {
            back = false;
            string input = Console.ReadLine();
            if (!int.TryParse(input, out value) || value < 1 || value > array.Length || string.IsNullOrEmpty(input))
            {
                Console.WriteLine("\nВаш ввод некорректен, попробуйте заново.");
                return false;
            }
            
            // Проверки на валидность ввода.
            if (value > 0 && value < array.Length + 1)
            {
                Console.WriteLine($"\nВы выбрали: {array[value - 1].ToLower()}");
                if (value == array.Length)
                {
                    back = true;
                }
                
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Обновляет директорию с консоли.
        /// </summary>
        /// <param name="array">Массив с валидными поддиректориями.</param>
        /// <param name="stored">Текущая директория</param>
        /// <param name="update">Вызов какого-либо bool return-type метода.</param>
        /// <param name="save"><c>true</c>, если пользователь хочет сохранить директорию; иначе <c>false</c>.</param>
        /// <returns><c>true</c>, если директория валидна; иначе <c>false</c>.</returns>
        private bool GetDirectoryUpdate(in string[] array, in string stored, Func<string, bool> update, out bool save)
        {
            save = false;
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int value) || value < 1 || value > array.Length || string.IsNullOrEmpty(input))
            {
                Console.WriteLine("\nВаш ввод некорректен, попробуйте заново.");
                return false;
            }

            // Проверки на валидность ввода.
            if (value == array.Length - 1)
            {
                Console.WriteLine($"\nВы выбрали: {array[value - 1].ToLower()}");
                return update(Path.GetFullPath(Path.Combine(stored, "../")));
            }

            if (value == array.Length)
            {
                Console.WriteLine($"\nВы выбрали: {array[value - 1].ToLower()}");
                save = true;
                return update(stored);
            }
            
            if (value < array.Length + 1)
            {
                Console.WriteLine($"\nВы выбрали: {array[value - 1]}");
                return update(Path.Combine(stored, array[value - 1]));
            }

            return false;
        }

        /// <summary>
        /// Подменяет расширение файла.
        /// </summary>
        /// <param name="extension">Расширение для подмены.</param>
        /// <param name="file">По ссылке меняет расширение файла.</param>
        private void SwapExtension(string extension, ref string file)
        {
            if (file.EndsWith(extension))
            {
                return;
            }

            file = string.Concat(file, extension);
        }
        
        /// <summary>
        /// Получает выбранную пользователем операцию.
        /// </summary>
        private void GetOperation()
        {
            while (true)
            {
                UpdateCurrentDirectory();
                Console.WriteLine("\nТекущая директория: \n" +
                                  $"{_currentDirectory}");
                
                PrintData("\nВыберите операцию, которую хотите совершить.", in _operations);
                _files = FileSystem.GetFilesInDirectory(_currentDirectory, "");
                _textFiles = FileSystem.GetFilesInDirectory(_currentDirectory, "*.txt");

                // Если получили валидное значение - выходим.
                if (GetInput(in _operations, out _currentOperation, out _))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Получает выбранную пользователем кодировку.
        /// </summary>
        /// <param name="back">Передает <c>true</c>, если пользователь хочет выйти; иначе <c>false</c>.</param>
        private void GetEncoding(out bool back)
        {
            back = false;
            while (true)
            {
                PrintData("\nВыберите кодировку для файла.", in _encodings);
                
                // Если получили валидное значение - выходим.
                if (GetInput(in _encodings, out _currentEncoding, out back))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Дает выбрать файл для действия.
        /// </summary>
        /// <param name="action">Имя действия.</param>
        /// <param name="back">Передает <c>true</c>, если пользователь хочет выйти; иначе <c>false</c>.</param>
        private void GetFileToAction(string action, out bool back)
        {
            back = false;
            while (true)
            {
                PrintData($"\nВыберите файл для {action}.", in _files);
                
                // Если получили валидное значение - выходим.
                if (GetInput(in _files, out _currentFile, out back))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Дает выбрать текстовый файл для действия.
        /// </summary>
        /// <param name="action">Имя действия.</param>
        /// <param name="back">Передает <c>true</c>, если пользователь хочет выйти; иначе <c>false</c>.</param>
        private void GetTextFileToAction(string action, out bool back)
        {
            back = false;
            while (true)
            {
                PrintData($"\nВыберите файл для {action}.", in _textFiles);
                
                // Если получили валидное значение - выходим.
                if (GetInput(in _textFiles, out _currentTextFile, out back))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Получает подтверждение совершаемого действия.
        /// </summary>
        /// <param name="action">Имя действия</param>
        /// <returns><c>true</c>, если получено подтверждение; иначе <c>false</c>.</returns>
        private bool GetActionConfirmation(string action)
        {
            Console.WriteLine($"\nВы уверены, что хотите {action} этот файл?" +
                              "\nВведите Y или y, чтобы подтвердить ваше действие." +
                              "\nИначе введите любую другую последовательность символов.");
            Console.Write("\nВвод: ");
            
            string input = Console.ReadLine();
            return input == "Y" || input == "y";
        }
        
        /// <summary>
        /// Выбирает диск для продолжения работы.
        /// </summary>
        private void SelectDisk()
        {
            while (true)
            {
                while (true)
                {
                    UpdateDisks();
                    if (_disks == null)
                    {
                        Console.WriteLine("\nНе удалось получить список дисков. Пожалуйста, введите абсолютный путь, " +
                                          "по которому вы хотите перейти.");
                        Console.Write("Ввод: ");
                        GetDirectoryNameFromConsole(out string directoryName);

                        // Ставим путь с консоли.
                        if (FileSystem.DirectoryExists(ref directoryName))
                        {
                            UpdateCurrentDirectory(directoryName);
                            break;
                        }

                        Console.WriteLine("\nТакой путь не существует, попробуйте заново.");
                        continue;
                    }

                    PrintData("\nВыберите диск, на котором хотите продолжить работу.", in _disks);
                    
                    // Если получили валидное значение - выходим.
                    if (GetDirectoryUpdate(in _disks, in _currentDirectory, UpdateCurrentDirectory, out _))
                    {
                        break;
                    }
                }

                break;
            }
        }

        /// <summary>
        /// Выбирает директорию для создания.
        /// </summary>
        private void SelectDirectoryFromConsole()
        {
            while (true)
            {
                PrintData("\nВведите название директории, которую хотите создать для перемещения " +
                          "(можно вводить относительные и абсолютные пути)", Array.Empty<string>());
                if (GetDirectoryNameFromConsole(out string directoryName))
                {
                    if (!FileSystem.ValidateDirectory(ref directoryName))
                    {
                        continue;
                    }
                    
                    // Создаем директорию и обновляем текущую.
                    if (FileSystem.CreateDirectory(directoryName, out _currentDirectory))
                    {
                        UpdateCurrentDirectory(_currentDirectory);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Выбирает директорию для перехода.
        /// </summary>
        /// <param name="call">Вызов какого-либо void return-type метода.</param>
        private void SelectDirectory(Action call)
        {
            while (true)
            {
                call();
                PrintData("\nТекущая директория: " + $"\n{_currentDirectory}" +
                          "\nВыберите директорию, в которую хотите перейти.", in _currentSubDirectories);
                

                // Кэшируем текущую директорию на случай если не удастся выбрать новую.
                string cachedDirectory = _currentDirectory;
                if (!GetDirectoryUpdate(in _currentSubDirectories, in _currentDirectory, UpdateCurrentDirectory,
                        out bool save))
                {
                    UpdateCurrentDirectory(cachedDirectory);
                    break;
                }

                // Выходим из цикла, если пользователь хочет сохраниться.
                if (save)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Выводит список файлов в текущей директории.
        /// </summary>
        private void ViewFiles()
        {
            // Проверка на валидность директории.
            if (_files is null)
            {
                FreezeOutput();
                return;
            }

            if (_files.Length == 1)
            {
                Console.WriteLine("\nВ текущей директории нет файлов.");
                FreezeOutput();
                return;
            }
            
            PrintData("\nВ текущей директории содержатся следующие файлы.", in _files);
            while (true)
            {
                _ = Console.ReadLine();
                break;
            }
        }

        /// <summary>
        /// Получает маску для поиска файлов.
        /// </summary>
        private void GetMask()
        {
            while (true)
            {
                PrintData("\nВведите маску для поиска файлов.", Array.Empty<string>());
                _currentMask = Console.ReadLine();
                
                if (string.IsNullOrEmpty(_currentMask))
                {
                    Console.WriteLine("\nВаш ввод некорректен, попробуйте заново.");
                    continue;
                }

                break;
            }
        }
        
        /// <summary>
        /// Получает список файлов по маске.
        /// </summary>
        /// <param name="search">Тип поиска - по всем поддиректориям или по корневой директории.</param>
        private void ViewFilesByMask(SearchOption search)
        {
            while (true)
            {
                GetMask();

                _maskFiles = FileSystem.GetFilesInDirectory(_currentDirectory, _currentMask, 
                    search == SearchOption.AllDirectories);
                
                // Проверка на валидность директории.
                if (_maskFiles is null)
                {
                    FreezeOutput();
                    break;
                }
                
                if (_maskFiles.Length == 1)
                {
                    Console.WriteLine("\nПо вашей маске не удалось найти файлы.");
                    FreezeOutput();
                    break;
                }
                
                PrintData("\nПо данной маске были найдены следующие файлы.", 
                    in _maskFiles);
                _ = Console.ReadLine();
                break;
            }
        }
        
        /// <summary>
        /// Перемещает файл в другую директорию.
        /// </summary>
        private void MoveFile()
        {
            while (true)
            {
                // Проверка на валидность директории.
                if (_files is null)
                {
                    FreezeOutput();
                    break;
                }
                
                if (_files.Length == 1)
                {
                    Console.WriteLine("\nВ текущей директории нет доступных файлов для перемещения.");
                    FreezeOutput();
                    break;
                }

                GetFileToAction("перемещения", out bool back);
                if (back)
                {
                    break;
                }
                
                var fs = new FileSystem(_files[_currentFile - 1]);
                void CallArrayUpdate()
                {
                    _currentSubDirectories[^1] = "Подтвердить выбор директории для перемещения";
                }
                // Выбираем директорию для перемещения и подменяет строку в массиве.
                SelectDirectory(CallArrayUpdate);

                if (GetActionConfirmation("переместить"))
                {
                    fs.MoveFile(_currentDirectory);
                    FreezeOutput();
                }

                break;
            }
        }

        /// <summary>
        /// Копирует файл.
        /// </summary>
        private void CopyFile()
        {
            while (true)
            {
                // Проверка на валидность директории.
                if (_files is null)
                {
                    FreezeOutput();
                    break;
                }
                
                if (_files.Length == 1)
                {
                    Console.WriteLine("\nВ текущей директории нет доступных файлов для копирования.");
                    FreezeOutput();
                    break;
                }

                GetFileToAction("копирования", out bool back);
                if (back)
                {
                    break;
                }
                
                var fs = new FileSystem(_files[_currentFile - 1]);
                void CallArrayUpdate()
                {
                    _currentSubDirectories[^1] = "Подтвердить выбор директории для копирования";
                }
                // Выбираем директорию для перемещения и подменяет строку в массиве.
                SelectDirectory(CallArrayUpdate);
                
                if (GetActionConfirmation("скопировать"))
                {
                    fs.CopyFile(_currentDirectory, false);
                    FreezeOutput();
                }

                break;
            }
        }

        /// <summary>
        /// Удаляет файл.
        /// </summary>
        private void DeleteFile()
        {
            while (true)
            {
                // Проверка на валидность директории.
                if (_files is null)
                {
                    FreezeOutput();
                    break;
                }
                
                if (_files.Length == 1)
                {
                    Console.WriteLine("\nВ текущей директории нет доступных файлов для удаления.");
                    FreezeOutput();
                    break;
                }

                GetFileToAction("удаления", out bool back);
                if (back)
                {
                    break;
                }

                var fs = new FileSystem(_files[_currentFile - 1]);
                if (GetActionConfirmation("удалить"))
                {
                    fs.DeleteFile();
                    FreezeOutput();
                }
                
                break;
            }
        }

        /// <summary>
        /// Читает текстовый файл в выбранной кодировке.
        /// </summary>
        private void ViewTextFile()
        {
            while (true)
            {
                // Проверка на валидность директории.
                if (_textFiles is null)
                {
                    FreezeOutput();
                    break;
                }
                
                if (_textFiles.Length == 1)
                {
                    Console.WriteLine("\nВ текущей директории нет доступных текстовых файлов для чтения.");
                    FreezeOutput();
                    break;
                }

                GetTextFileToAction("чтения", out bool back);
                if (back)
                {
                    break;
                }

                GetEncoding(out back);
                if (back)
                {
                    break;
                }

                var fs = new FileSystem(_textFiles[_currentTextFile - 1]);
                fs.ReadTextFile(_trueEncodings[_currentEncoding - 1]);
                FreezeOutput();
                break;
            }
        }

        /// <summary>
        /// Создает текстовый файл в выбранной кодировке.
        /// </summary>
        private void CreateTextFile()
        {
            while (true)
            {
                string fileName;
                while (true)
                {
                    PrintData("\nВведите название файла, который вы хотите создать.", Array.Empty<string>());
                    if (GetFileNameFromConsole(out fileName))
                    {
                        break;
                    }
                }

                GetEncoding(out bool back);
                if (back)
                {
                    break;
                }
                
                PrintData($"\nВведите текст, который хотите записать в {fileName}.", Array.Empty<string>());
                GetTextFromConsole(out string text);
                
                // Подменяем расширение, если оно не .txt
                SwapExtension(".txt", ref fileName);
                var fs = new FileSystem(Path.Combine(_currentDirectory, fileName));
                fs.CreateTextFile(text, _trueEncodings[_currentEncoding - 1]);
                FreezeOutput();
                break;
            }
        }

        /// <summary>
        /// Соединяет несколько файлов воедино.
        /// </summary>
        private void BuildConcat()
        {
            var filesConcat = new StringBuilder();
            while (true)
            {
                _textFiles[^1] = "Закончить конкатенацию файлов";
                while (true)
                {
                    PrintData("\nВыберите файл для чтения.", in _textFiles);
                    if (GetInput(in _textFiles, out _currentTextFile, out _))
                    {
                        break;
                    }
                }

                // Конец конкатенации, выход из цикла.
                if (_textFiles[_currentTextFile - 1] == _textFiles[^1])
                {
                    break;
                }
                    
                var fs = new FileSystem(_textFiles[_currentTextFile - 1]);
                fs.ReadTextFile(_trueEncodings[_currentEncoding - 1], out string text);
                filesConcat.Append(text + "\n");
            }
                
            Console.WriteLine("\nРезультат сложения содержимого файлов: " +
                              $"\n{filesConcat}");
            FreezeOutput();
        }
        
        /// <summary>
        /// Показывает результат соединения файлов.
        /// </summary>
        private void ConcatFiles()
        {
            while (true)
            {
                // Проверка на валидность директории.
                if (_textFiles is null)
                {
                    FreezeOutput();
                    break;
                }
                
                if (_textFiles.Length < 3)
                {
                    Console.WriteLine("\nВ текущей директории меньше двух доступных текстовых файлов для чтения. ");
                    FreezeOutput();
                    break;
                }
                
                GetEncoding(out bool back);
                if (back)
                {
                    break;
                }
                
                BuildConcat();
                break;
            }
        }

        /// <summary>
        /// Получает информацию о file overwrite.
        /// </summary>
        /// <param name="pathFirst">Первый путь.</param>
        /// <param name="pathSecond">Второй путь</param>
        /// <returns><c>true</c>, если пользователь хочет перезаписать файл; иначе <c>false</c>.</returns>
        private bool GetOverWrite(string pathFirst, string pathSecond)
        {
            while (true)
            {
                string[] overWriteOptions = {"Перезаписать файл", "Оставить текущий файл"};
                PrintData($"\nНайдено совпадение между {pathFirst} и {pathSecond}." +
                          $"\nВыберите действие.", overWriteOptions);

                if (GetInput(in overWriteOptions, out int index, out _))
                {
                    return index == 1;
                }
            }
        }

        /// <summary>
        /// Получает тип выбора директории.
        /// </summary>
        private void GetDirectoryChooseType()
        {
            while (true)
            {
                string[] dirChooseType = {"Выбрать из уже существующих", "Создать новую по абсолютному пути"};
                PrintData("\nВыберите то, в какую директорию вы хотите скопировать файлы.", in dirChooseType);

                if (GetInput(in dirChooseType, out int index, out _))
                {
                    if (index == 1)
                    {
                        void CallArrayUpdate()
                        {
                            _currentSubDirectories[^1] = "Подтвердить выбор директории для перемещения в нее файлов";
                        }
                        SelectDirectory(CallArrayUpdate);
                    }
                    else
                    {
                        SelectDirectoryFromConsole();
                    }

                    break;
                }
            }
        }
        
        /// <summary>
        /// Копирует все файлы в другую директорию по заданной маске.
        /// </summary>
        private void CopyAllFiles()
        {
            while (true)
            {
                GetMask();
                _allFiles = FileSystem.GetFilesInDirectory(_currentDirectory, _currentMask, true);

                if (_allFiles is null)
                {
                    FreezeOutput();
                    break;
                }
                
                if (_allFiles.Length == 1)
                {
                    Console.WriteLine("\nВ текущей директории нет файлов, соответствующих заданной маске, " +
                                      "попробуйте заново.");
                    FreezeOutput();
                    break;
                }
                
                GetDirectoryChooseType();
                // Цикл для копирования нескольких файлов.
                for (int i = 0; i < _allFiles.Length - 1; i++)
                {
                    var file = new FileSystem(_allFiles[i]);
                    string newPath = Path.Combine(_currentDirectory, file.GetFileName());
                    if (File.Exists(newPath))
                    {
                        bool overWrite = GetOverWrite(file.GetFilePath(), newPath);
                        file.CopyFile(_currentDirectory, overWrite);
                        continue;
                    }

                    file.CopyFile(_currentDirectory, false);
                }

                FreezeOutput();
                break;
            }
        }

        /// <summary>
        /// Старт программы.
        /// </summary>
        private void Start()
        {
            Console.WriteLine($"Добро пожаловать в файловый менеджер.\n " +
                              $"\nТекущая директория: " +
                              $"\n{_currentDirectory}");
            SelectDisk();
        }

        
        /// <summary>
        /// Вызов выбора диска.
        /// </summary>
        private void RunSelectDisk()
        {
            switch (_currentOperation)
            {
                case 1:
                    SelectDisk();
                    break;
            }
        }

        /// <summary>
        /// Вызов выбора директории.
        /// </summary>
        private void RunSelectDirectory()
        {
            switch (_currentOperation)
            {
                case 2:
                    SelectDirectory(EmptyCall);
                    break;
            }
        }

        /// <summary>
        /// Вызов просмотра списка файлов.
        /// </summary>
        private void RunViewFiles()
        {
            switch (_currentOperation)
            {
                case 3:
                    ViewFiles();
                    break;
            }
        }
        
        /// <summary>
        /// Вызов просмотра списка файлов по маске.
        /// </summary>
        private void RunViewFilesByMask()
        {
            switch (_currentOperation)
            {
                case 4:
                    ViewFilesByMask(SearchOption.TopDirectoryOnly);
                    break;
                case 5:
                    ViewFilesByMask(SearchOption.AllDirectories);
                    break;
            }
        }
        
        /// <summary>
        /// Вызов чтения текстового файла.
        /// </summary>
        private void RunViewTextFile()
        {
            switch (_currentOperation)
            {
                case 6: 
                    ViewTextFile();
                    break;
            }
        }

        /// <summary>
        /// Вызов создания текстового файла.
        /// </summary>
        private void RunCreateTextFile()
        {
            switch (_currentOperation)
            {
                case 7:
                    CreateTextFile();
                    break;
            }
        }

        /// <summary>
        /// Вызов копирования файла.
        /// </summary>
        private void RunCopyFile()
        {
            switch (_currentOperation)
            {
                case 8:
                    CopyFile();
                    break;
            }
        }

        /// <summary>
        /// Вызов перемещения файла.
        /// </summary>
        private void RunMoveFile()
        {
            switch (_currentOperation)
            {
                case 9:
                    MoveFile();
                    break;
            }
        }

        /// <summary>
        /// Вызов удаления файла.
        /// </summary>
        private void RunDeleteFile()
        {
            switch (_currentOperation)
            {
                case 10:
                    DeleteFile();
                    break;
            }
        }
        
        /// <summary>
        /// Вызов конкатенации файлов.
        /// </summary>
        private void RunConcatFiles()
        {
            switch (_currentOperation)
            {
                case 11:
                    ConcatFiles();
                    break;
            }
        }

        /// <summary>
        /// Вызов перемещения всех файлов из директории и поддиректорий.
        /// </summary>
        private void RunMoveAllFiles()
        {
            switch (_currentOperation)
            {
                case 12:
                    CopyAllFiles();
                    break;
            }
        }
        
        /// <summary>
        /// Вызов завершения программы.
        /// </summary>
        private void RunExit()
        {
            switch (_currentOperation)
            {
                case 13:
                    Exit();
                    break;
            }
        }
        
        /// <summary>
        /// Исполнение операции.
        /// </summary>
        private void RunOperation()
        {
            RunSelectDisk();
            RunSelectDirectory();
            RunViewFiles();
            RunViewFilesByMask();
            RunViewTextFile();
            RunCreateTextFile();
            RunCopyFile();
            RunMoveFile();
            RunDeleteFile();
            RunConcatFiles();
            RunMoveAllFiles();
            RunExit();
        }

        /// <summary>
        /// Итерационный вызов операций.
        /// </summary>
        public void Run()
        {
            Start();
            
            while (true)
            {
                GetOperation();
                RunOperation();

                // Если пользователь решил закончить работу, то прерываем цикл.
                if (_currentOperation == 13)
                {
                    break;
                }
            }
        }
    }
}