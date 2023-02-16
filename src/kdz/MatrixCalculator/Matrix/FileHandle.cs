using System;
using System.IO;

namespace Matrix
{
    /// <summary>
    /// Класс для получения чисел и матриц из файла.
    /// </summary>
    public class FileHandle
    {
        /// <summary>
        /// Имя читаемого файла.
        /// </summary>
        private string FileName { get; }
        
        /// <summary>
        /// Конструктор, который получает имя файла, которое нужно прочитать.
        /// </summary>
        /// <param name="fileName">Строка, содержащая имя файла.</param>
        public FileHandle(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Метод для преобразования строки в целое число с ограничением снизу и сверху.
        /// </summary>
        /// <param name="input">Строка, которая будет конвертирована в число.</param>
        /// <param name="result">После вызова метода передает числовое значение по ссылке.</param>
        /// <returns><c>true</c>, если удалось преобразовать строку в число в диапазоне; иначе <c>false</c>.</returns>
        private bool IsInBounds(string input, out int result)
        {
            if (!int.TryParse(input, out result))
            {
                return false;
            }

            if (result < 1 || result > 10)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Получает количество строк/cтолбцов из строк.
        /// </summary>
        /// <param name="sRows">Строка с количеством строк в матрице.</param>
        /// <param name="sColumns">Строка с количеством столбцов в матрице.</param>
        /// <param name="rows">После вызова метода передает количество строк по ссылке.</param>
        /// <param name="columns">После вызова метода передает количество столбцов по ссылке.</param>
        /// <returns><c>true</c>, если удалось преобразовать строки в числа в диапазоне; иначе <c>false</c>.</returns>
        private bool GetRowsAndColumns(string sRows, string sColumns, out int rows, out int columns)
        {
            if (!IsInBounds(sRows, out rows) | !IsInBounds(sColumns, out columns))
            {
                Console.WriteLine($"\nНе удалось получить количество строк/столбцов матрицы из файла {FileName}.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Получает значения из строки.
        /// </summary>
        /// <param name="row">Строка с числами.</param>
        /// <param name="currentRow">Номер текущей строки матрицы.</param>
        /// <param name="columns">Количество столбцов в матрице.</param>
        /// <param name="values">После вызова метода передает значения в массиве double[] по ссылке.</param>
        /// <returns><c>true</c>, если удалось преобразовать строку в массив double[]; иначе <c>false</c>.</returns>
        private bool GetRowValues(string row, int currentRow, int columns, out double[] values)
        {
            values = new double[1];
            if (row is null || row.Length == 0)
            {
                Console.WriteLine($"\nВ файле {FileName}.txt присутствует пустая строка. " +
                                  "Исправьте матрицу и повторите попытку.");
                return false;
            }
            
            // Формирование массива строк
            string[] rowSplit = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            // Предпарсинг для учета разнолокального ввода.
            rowSplit = Parse.Comma(rowSplit);
            if (rowSplit.Length == 0 || rowSplit.Length != columns)
            {
                Console.WriteLine($"\nКоличество элементов в строке {currentRow + 1} в файле {FileName}.txt " +
                                  "не равно количеству столбцов или в строке содержатся недопустимые символы!\n" +
                                  "Исправьте матрицу и повторите попытку.");
                return false;
            }

            // Заполнение массива.
            values = new double[columns];
            for (int i = 0; i < columns; i++)
            {
                if (!double.TryParse(rowSplit[i], out values[i]))
                { 
                    Console.WriteLine($"\nЭлемент строки {currentRow + 1}, столбца {i + 1} файла {FileName}.txt " +
                                      "невозможно привести к числу! Исправьте матрицу и повторите попытку.");
                    return false;
                }

                values[i] = Math.Round(values[i], 3);
            }

            return true;
        }

        /// <summary>
        /// Заполняет матрицу объекта типа <c>Matrix</c>.
        /// </summary>
        /// <param name="rows">Массив строк для парсинга.</param>
        /// <param name="matrix">Ссылка на объект типа <c>Matrix</c> для заполнения матрицы.</param>
        /// <returns></returns>
        private bool GetFilledMatrix(string[] rows, ref Matrix matrix)
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (!GetRowValues(rows[i], i, matrix.Columns, out double[] values))
                {
                    return false;
                }

                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix.Mat[i][j] = values[j];
                }
            }

            return true;
        }

        /// <summary>
        /// Чтение матрицы с файла.
        /// </summary>
        /// <param name="lines">После вызова метода передает строки в массиве string[] по ссылке.</param>
        /// <returns><c>true</c>, если удалось прочитать файл; иначе <c>false</c>.</returns>
        private bool CallFileHandler(out string[] lines)
        {
            try
            {
                lines = File.ReadAllLines($"../../../{FileName}.txt");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось прочитать файл {FileName}.txt. Убедитесь, что файл существует и читаем, " +
                                  "а после повторите попытку.");
                lines = new string[1];
                return false;
            }
        }

        /// <summary>
        /// Чтение числа с файла.
        /// </summary>
        /// <param name="number">После вызова метода передает числовое значение по ссылке.</param>
        /// <returns><c>true</c>, если удалось получить число из файла; иначе <c>false</c>.</returns>
        public bool GetNumber(out double number)
        {
            try
            {
                number = 0;
                // Чтение файла.
                if (!CallFileHandler(out string[] lines))
                {
                    return false;
                }
                
                if (lines.Length == 0)
                {
                    Console.WriteLine($"\nФайл {FileName}.txt пуст. Исправьте содержимое файла и повторите попытку.");
                    return false;
                }
                
                // Перевод строки в число.
                if (!double.TryParse(Parse.Comma(lines[0]), out number))
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось получить число из файла {FileName}.txt. Исправьте содержимое " +
                                  "файла и повторите попытку.");
                number = 0;
                return false;
            }
        }

        /// <summary>
        /// Чтение матрицы с файла.
        /// </summary>
        /// <param name="matrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        /// <returns><c>true</c>, если удалось получить матрицу из файла; иначе <c>false</c>.</returns>
        public bool GetMatrix(out Matrix matrix)
        {
            try
            {
                matrix = new Matrix(1, 1);
                if (!CallFileHandler(out string[] lines))
                {
                    return false;
                }

                if (!GetRowsAndColumns(lines[0], lines[1], out int rows, out int columns))
                {
                    return false;
                }

                matrix = new Matrix(rows, columns);
                lines = lines[2..lines.Length];
                if (!GetFilledMatrix(lines, ref matrix))
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"\nНе удалось получить матрицу из файла {FileName}.txt. Исправьте содержимое " +
                                  "файла и повторите попытку.");
                matrix = new Matrix(1, 1);
                return false;
            }
        }
    }
}