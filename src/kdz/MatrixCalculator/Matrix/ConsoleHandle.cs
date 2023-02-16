using System;

namespace Matrix
{
    /// <summary>
    /// Класс для получения чисел и матриц из консоли.
    /// </summary>
    public class ConsoleHandle
    {
        /// <summary>
        /// Метод для преобразования строки в целое число с ограничением снизу и сверху.
        /// </summary>
        /// <param name="input">Строка, которая будет конвертирована в число.</param>
        /// <param name="value">После вызова метода передает числовое значение по ссылке.</param>
        /// <returns><c>true</c>, если удалось преобразовать строку в число в диапазоне; иначе <c>false</c>.</returns>
        private bool GetLimitedValue(string input, out int value)
        {
            if (!int.TryParse(input, out value))
            {
                Console.WriteLine("Ваш ввод нельзя распознать как число! Повторите попытку.\n");
                return false;
            }

            if (value < 1 || value > 10)
            {
                Console.WriteLine("Ваше число не попало в допустимый диапазон! Повторите попытку.\n");
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Метод для получения количества строк и столбцов из консоли.
        /// </summary>
        /// <param name="rows">После вызова метода передает количество строк по ссылке.</param>
        /// <param name="columns">После вызова метода передает количество столбцов по ссылке.</param>
        private void GetRowsAndColumns(out int rows, out int columns)
        {
            while (true)
            {
                Console.WriteLine("\nВведите количество строк в матрице (от 1 до 10).");
                Console.Write("Ввод: ");
                
                if (!GetLimitedValue(Console.ReadLine(), out rows))
                {
                    continue;
                }
                
                Console.WriteLine("\nВведите количество столбцов в матрице (от 1 до 10).");
                Console.Write("Ввод: ");

                if (!GetLimitedValue(Console.ReadLine(), out columns))
                {
                    continue;
                }

                Console.WriteLine($"Выбранный размер: {rows}x{columns}\n");
                break;
            }
        }

        /// <summary>
        /// Получение значений из построчного ввода.
        /// </summary>
        /// <param name="input">Строка с числами.</param>
        /// <param name="columns">Ограничение на количество числе в строке.</param>
        /// <param name="values">После вызова метода передает числа внутри массива double[] по ссылке.</param>
        /// <returns><c>true</c>, если удалось получить значения; иначе <c>false</c>.</returns>
        private bool GetRowValues(string input, int columns, out double[] values)
        {
            values = new double[columns];
            if (input is null || input.Length == 0)
            {
                Console.WriteLine("\nВаша строка пуста или не определена! Повторите попытку.");
                return false;
            }
            
            // Формирование массива строк
            string[] inputSplit = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            // Предпарсинг для учета разнолокального ввода.
            inputSplit = Parse.Comma(inputSplit);
            if (inputSplit.Length == 0 || inputSplit.Length != columns)
            {
                Console.WriteLine("\nКоличество элементов в строке не равно количеству столбцов " +
                                  "или в строке содержатся недопустимые символы!\n" +
                                  "Повторите попытку.");
                return false;
            }

            // Заполнение массива.
            values = new double[columns];
            for (int i = 0; i < columns; i++)
            {
                if (!double.TryParse(inputSplit[i], out values[i]))
                {
                    Console.WriteLine($"\nНе удалось преобразовать {i + 1} элемент строки в число. Повторите попытку!");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Заполнение матрицы объекта типа <c>Matrix</c>.
        /// </summary>
        /// <param name="rows">Количество строк в матрице.</param>
        /// <param name="columns">Количество столбцов в матрице.</param>
        /// <param name="matrix">Передает по ссылке объект типа <c>Matrix</c> для заполнения.</param>
        private void FillMatrix(int rows, int columns, ref Matrix matrix)
        {
            for (int i = 0; i < rows; i++)
            {
                while (true)
                {
                    Console.Write("Ввод: ");
                    // Пока не удалось получить с консоли валидный ввод, возвращаемся в начало блока.
                    if (!GetRowValues(Console.ReadLine(), columns, out double[] arrayFilled))
                    {
                        continue;
                    }
                    
                    for (int j = 0; j < columns; j++)
                    {
                        matrix.Mat[i][j] = arrayFilled[j];
                    }

                    break;
                }
            }
        }
        
        /// <summary>
        /// Получает объект типа <c>Matrix</c> с консоли.
        /// </summary>
        /// <param name="rows">Количество строк в матрице.</param>
        /// <param name="columns">Количество столцов в матрице.</param>
        /// <param name="matrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        private void GetMatrixFromConsole(int rows, int columns, out Matrix matrix)
        {
            matrix = new Matrix(rows, columns);
            Console.WriteLine($"\nВведите матрицу построчно. Всего строк - {rows}, " +
                              $"элементов в каждой строке - {columns}. После ввода каждой строки нажмите ENTER.");
            
            FillMatrix(rows, columns, ref matrix);
        }

        /// <summary>
        /// Получает число с консоли.
        /// </summary>
        /// <param name="number">После вызова метода передает числовое значение по ссылке.</param>
        private void GetNumberFromConsole(out double number)
        {
            Console.WriteLine("\nВведите число, на которое хотите умножить матрицу.");
            while (true)
            {
                Console.Write("Ввод: ");
                
                // Пока не удалось получить с консоли валидный ввод, возвращаемся в начало блока.
                if (!double.TryParse(Parse.Comma(Console.ReadLine()), out number))
                {
                    Console.WriteLine("\nНе удалось преобразовать ваш ввод в число! Повторите попытку.");
                    continue;
                }

                break;
            }
        }
        
        /// <summary>
        /// Инициализирует полученными с консоли значениями и передает объект типа <c>Matrix</c>.
        /// </summary>
        /// <param name="matrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        public void RunConsoleHandler(out Matrix matrix)
        {
            GetRowsAndColumns(out int rows, out int columns);
            GetMatrixFromConsole(rows, columns, out matrix);
        }

        /// <summary>
        /// Получает числовое значение с консоли и передает его.
        /// </summary>
        /// <param name="number">После вызова метода передает числовое значение по ссылке.</param>
        public void RunConsoleHandler(out double number)
        {
            GetNumberFromConsole(out number);
        }
    }
}