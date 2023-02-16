using System;

namespace Matrix
{
    /// <summary>
    /// Класс для генерации рандомных матриц внутри объекта типа <c>Matrix</c>.
    /// </summary>
    public class GenerateHandle
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
        /// Получает количество и столбцов в генерируемой матрице.
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

                Console.WriteLine($"Выбранный размер: {rows}x{columns}");
                break;
            }
        }
        
        /// <summary>
        /// Инициализирует объект типа <c>Matrix</c> и заполняет матрицу рандомными значениями.
        /// </summary>
        /// <param name="matrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        public void RunGeneratorHandler(out Matrix matrix)
        {
            GetRowsAndColumns(out int rows, out int columns);
            matrix = new Matrix(rows, columns, true);
        }
    }
}