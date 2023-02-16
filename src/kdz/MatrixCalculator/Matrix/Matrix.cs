using System;
using System.Globalization;

namespace Matrix
{
    /// <summary>
    /// Класс для работы с матрицами.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Количество строк в матрице.
        /// </summary>
        public int Rows { get; set; }
        
        /// <summary>
        /// Количество столбцов в матрице.
        /// </summary>
        public int Columns { get; set; }
        
        /// <summary>
        /// Матрица в типе double[][].
        /// </summary>
        public double[][] Mat { get; set; }

        /// <summary>
        /// Конструктор, которые создает объект из строк и столбцов.
        /// </summary>
        /// <param name="rows">Количество строк</param>
        /// <param name="columns">Количество столбцов</param>
        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            // Создание массива double[][].
            Mat = new double[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                Mat[i] = new double[Columns];
            }
        }

        /// <summary>
        /// Конструктор, который создает объект из строк и столбцов и рандомно заполняет массив double[][]
        /// </summary>
        /// <param name="rows">Количество строк</param>
        /// <param name="columns">Количество столбцов</param>
        /// <param name="random">Заполнение рандомно, если <c>true</c>; иначе заполнение нулями.</param>
        public Matrix(int rows, int columns, bool random)
        {
            Rows = rows;
            Columns = columns;

            // Создание массива double[][].
            Mat = new double[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                Mat[i] = new double[Columns];
                // Заполнение рандомными значениями.
                if (random)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        Mat[i][j] = RandomValue();
                    }
                }
            }
        }
        
        /// <summary>
        /// Конструктор, который создает объект из строк и столбцов и заполняет из массива double[][]
        /// </summary>
        /// <param name="rows">Количество строк</param>
        /// <param name="columns">Количество столбцов</param>
        /// <param name="matrix">Массив double[][] для заполнения.</param>
        public Matrix(int rows, int columns, double[][] matrix)
        {
            Rows = rows;
            Columns = columns;

            // Создание массива double[][].
            Mat = new double[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                Mat[i] = new double[Columns];
                // Заполнение из массива.
                for (int j = 0; j < Columns; j++)
                {
                    Mat[i][j] = Math.Round(matrix[i][j], 3);
                }
            }
        }

        /// <summary>
        /// Конструктор, который копирует объект.
        /// </summary>
        /// <param name="matrix">Объект типа <c>Matrix</c> для создания копии.</param>
        public Matrix(Matrix matrix)
        {
            Rows = matrix.Rows;
            Columns = matrix.Columns;
            
            // Создание массива double[][]
            Mat = new double[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                Mat[i] = new double[Columns];
                // Заполнение из double[][] объекта.
                for (int j = 0; j < Columns; j++)
                {
                    Mat[i][j] = Math.Round(matrix.Mat[i][j], 3);
                }
            }
        }

        /// <summary>
        /// Генерация рандомного значения в типе double (от -1000 до 1000).
        /// </summary>
        /// <returns>Возвращает сгенерированное значение в типе double.</returns>
        private double RandomValue()
        {
            var randomize = new Random();
            int[] randomRound = {0, 3};
            int[] randomSign = {-1, 1};
            
            // Генерация дробной части (может быть 0.0 или с точностью до трех знаков после запятой)
            double floatingPart = Math.Round(randomize.NextDouble(), randomRound[randomize.Next(randomRound.Length)]);
            // Генерация целой части (от -1000 до 1000)
            double integerPart = randomSign[randomize.Next(randomSign.Length)] * randomize.Next(1000);
            
            return floatingPart + integerPart;
        }

        /// <summary>
        /// Меняет местами две строки в матрице.
        /// </summary>
        /// <param name="matrix">Объект для изменения строк.</param>
        /// <param name="firstRow">Первая строка для изменения.</param>
        /// <param name="secondRow">Вторая строка для изменения.</param>
        private void SwapRows(ref Matrix matrix, int firstRow, int secondRow)
        {
            (matrix.Mat[firstRow], matrix.Mat[secondRow]) = (matrix.Mat[secondRow], matrix.Mat[firstRow]);
        }

        /// <summary>
        /// Печатает матрицу, хранящуюся в объекте.
        /// </summary>
        public void Print()
        {
            // Получает максимальную длину числа в столбце.
            int[] lengths = new int[Columns];
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    int columnLength = Mat[j][i].ToString(CultureInfo.InvariantCulture).Length;
                    if (columnLength >= lengths[i])
                    {
                        lengths[i] = columnLength;
                    }
                }
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    int currentLength = Mat[i][j].ToString(CultureInfo.InvariantCulture).Length;
                    int lengthDifference = Math.Abs(currentLength - lengths[j]);
                    string toPrint = Mat[i][j].ToString(CultureInfo.InvariantCulture);
                    
                    // Выравнивание по самому длинному элементу столбца.
                    Console.Write(toPrint.PadLeft(currentLength + lengthDifference, ' ') + " ");
                    if (j == Columns - 1)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        /// <summary>
        /// Получает след матрицы.
        /// </summary>
        /// <param name="matrixTrace">После вызова метода передает значение следа матрицы по ссылке.</param>
        /// <returns><c>true</c>, если можно извлечь след матрицы; иначе <c>false</c>.</returns>
        public bool GetTrace(out double matrixTrace)
        {
            matrixTrace = 0;
            // След только для квадратных матриц.
            if (Rows != Columns)
            {
                return false;
            }

            // Сумма элементов на диагонали
            for (int i = 0; i < Rows; i++)
            {
                matrixTrace += Mat[i][i];
            }

            matrixTrace = Math.Round(matrixTrace, 3);
            return true;
        }

        /// <summary>
        /// Получает транспозицию матрицы.
        /// </summary>
        /// <param name="matrixTransposed">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        /// <returns><c>true</c>, если можно получить транспозицию матрицы; иначе <c>false</c>.</returns>
        public bool GetTranspose(out Matrix matrixTransposed)
        {
            // Новый объект, над которым производятся операции.
            matrixTransposed = new Matrix(Columns, Rows);
            // Меняем элементы местами ([j][i] -> [i][j]).
            for (int i = 0; i < matrixTransposed.Rows; i++)
            {
                for (int j = 0; j < matrixTransposed.Columns; j++)
                {
                    matrixTransposed.Mat[i][j] = Mat[j][i];
                }
            }

            return true;
        }

        /// <summary>
        /// Получает сумму двух матриц.
        /// </summary>
        /// <param name="secondMatrix">Второй объект типа <c>Matrix</c>для суммирования.</param>
        /// <param name="sumMatrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        /// <returns><c>true</c>, если можно получить сумму матриц; иначе <c>false</c>.</returns>
        public bool GetSum(Matrix secondMatrix, out Matrix sumMatrix)
        {
            sumMatrix = new Matrix(Rows, Columns);
            // Сумма двух матриц определена только для матриц одинаковых размерностей.
            if (Rows != secondMatrix.Rows || Columns != secondMatrix.Columns)
            {
                return false;
            }

            // Поэлементное суммирование.
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sumMatrix.Mat[i][j] = Math.Round(Mat[i][j] + secondMatrix.Mat[i][j], 3);
                }
            }

            return true;
        }

        /// <summary>
        /// Получает сумму двух матриц.
        /// </summary>
        /// <param name="secondMatrix">Второй объект типа <c>Matrix</c>для вычитания.</param>
        /// <param name="diffMatrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        /// <returns><c>true</c>, если можно получить разность матриц; иначе <c>false</c>.</returns>
        public bool GetDifference(Matrix secondMatrix, out Matrix diffMatrix)
        {
            diffMatrix = new Matrix(Rows, Columns);
            // Разность двух матриц определена только для матриц одинаковых размерностей.
            if (Rows != secondMatrix.Rows || Columns != secondMatrix.Columns)
            {
                return false;
            }

            // Поэлементная разность.
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    diffMatrix.Mat[i][j] = Math.Round(Mat[i][j] - secondMatrix.Mat[i][j], 3);
                }
            }

            return true;
        }

        /// <summary>
        /// Получает произведение двух матриц.
        /// </summary>
        /// <param name="secondMatrix">Второй объект <c>Matrix</c> для умножения.</param>
        /// <param name="prodMatrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        /// <returns><c>true</c>, если можно получить произведение матриц; иначе <c>false</c>.</returns>
        public bool GetProduct(Matrix secondMatrix, out Matrix prodMatrix)
        {
            prodMatrix = new Matrix(Rows, secondMatrix.Columns);
            // Умножение двух матриц определено только в том случае, если количество столбцов первой матрицы 
            // совпадает с количеством строк второй матрицы.
            if (Columns != secondMatrix.Rows)
            {
                return false;
            }

            // По определению умножения матриц.
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < secondMatrix.Columns; j++)
                {
                    for (int k = 0; k < Columns; k++)
                    {
                        prodMatrix.Mat[i][j] += Mat[i][k] * secondMatrix.Mat[k][j];
                        prodMatrix.Mat[i][j] = Math.Round(prodMatrix.Mat[i][j], 3);
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// Получает произведение матрицы и числа.
        /// </summary>
        /// <param name="factor">Число, на которое будет умножена матрица.</param>
        /// <param name="prodNumMatrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        /// <returns><c>true</c>, если можно получить произведение числа и матрицы; иначе <c>false</c>.</returns>
        public bool GetProductForNumber(double factor, out Matrix prodNumMatrix)
        {
            prodNumMatrix = new Matrix(Rows, Columns);
            // По определению умножения на число.
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    prodNumMatrix.Mat[i][j] = Math.Round(Mat[i][j] * factor);
                }
            }

            return true;
        }

        /// <summary>
        /// Получает детерминант (определитель матрицы).
        /// </summary>
        /// <param name="detOfMatrix">После вызова метода передает значение детерминанта по ссылке.</param>
        /// <returns><c>true</c>, если можно получить детерминант матрицы; иначе <c>false</c>.</returns>
        public bool GetDeterminant(out double detOfMatrix)
        {
            detOfMatrix = 1;
            // Детерминант определен только для квадратных матриц.
            if (Rows != Columns)
            {
                return false;
            }

            // Создаем копию объекта, чтобы не портить введенную матрицу.
            var matrixCopy = new Matrix(this);
            // Приведем матрицу к ступеначтому виду, чтобы представить детерминант как произведение членов на диагонали.
            for (int i = 0; i < Rows; i++)
            {
                for (int j = i + 1; j < Rows; j++)
                {
                    // Меняем строки местами и знак детерминанта по определению, если элемент на диагонали равен нулю.
                    if (matrixCopy.Mat[i][i] == 0)
                    {
                        for (int k = i + 1; k < Rows; k++)
                        {
                            if (matrixCopy.Mat[k][i] != 0)
                            {
                                SwapRows(ref matrixCopy, i, k);
                                detOfMatrix = -detOfMatrix;
                            }
                        }
                    }

                    // Вычитаем коэффициент поэлементно.
                    if (matrixCopy.Mat[i][i] != 0)
                    {
                        double fraction = matrixCopy.Mat[j][i] / matrixCopy.Mat[i][i];
                        for (int k = i; k < Rows; k++)
                        {
                            matrixCopy.Mat[j][k] -= matrixCopy.Mat[i][k] * fraction;
                            if (Math.Abs(matrixCopy.Mat[j][k]) < double.Epsilon)
                            {
                                matrixCopy.Mat[j][k] = 0;
                            }
                        }
                    }
                }
            }

            // Умножаем элементы диагонали для извлечение детерминанта из ступенчатого вида.
            for (int i = 0; i < Rows; i++)
            {
                detOfMatrix *= matrixCopy.Mat[i][i];
            }

            detOfMatrix = Math.Abs(detOfMatrix) == 0 ? 0 : detOfMatrix;
            detOfMatrix = Math.Round(detOfMatrix, 3);
            return true;
        }

        /// <summary>
        /// Приводит матрицу к ступенчатому виду через метод Гаусса.
        /// </summary>
        /// <param name="matrix">Получает ссылку на объект, к которому применяется метод Гаусса.</param>
        /// <returns><c>true</c>, если удалось применить метод Гаусса; иначе <c>false</c>.</returns>
        private bool GetGauss(ref Matrix matrix)
        {
            try
            {
                // Начинаем итерации
                for (int i = 0; i < Rows; i++)
                {
                    double maxElement = Math.Abs(matrix.Mat[i][i]);
                    int maxRow = i;

                    // Находим максимальный элемент в верхнем треугольнике.
                    for (int j = i + 1; j < Rows; j++)
                    {
                        if (Math.Abs(matrix.Mat[j][i]) > maxElement)
                        {
                            maxElement = Math.Abs(matrix.Mat[j][i]);
                            maxRow = j;
                        }
                    }

                    // Меняем текущую и максимальную строку местами.
                    SwapRows(ref matrix, maxRow, i);
                    // Прибавляем коэффициент * элемент матрицы.
                    for (int j = i + 1; j < Rows; j++)
                    {
                        double fraction = -matrix.Mat[j][i] / matrix.Mat[i][i];
                        for (int k = i; k < Columns; k++)
                        {
                            if (i == k)
                            {
                                matrix.Mat[j][k] = 0;
                            }
                            else
                            {
                                matrix.Mat[j][k] += fraction * matrix.Mat[i][k];
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Извлекает решения после применения метода Гаусса к матрице.
        /// </summary>
        /// <param name="matrixCopy">После вызова метода передает объект с приведенной матрицей.</param>
        /// <returns><c>true</c>, если удалось извлечь решения; иначе <c>false</c>.</returns>
        public bool GetSolutions(out Matrix matrixCopy)
        {
            matrixCopy = new Matrix(this);
            // Применяем метод гаусса только если количество строк равно количеству неизвестных.
            if (Rows != Columns - 1)
            {
                return false;
            }

            if (!GetGauss(ref matrixCopy))
            {
                return false;
            }

            // Заменяем некорректные элементы на нули.
            for (int i = 0; i < matrixCopy.Rows; i++)
            {
                for (int j = 0; j < matrixCopy.Columns; j++)
                {
                    string currentElement = matrixCopy.Mat[i][j].ToString(CultureInfo.InvariantCulture);
                    if (currentElement == "NaN" || currentElement == "-Infinity" || currentElement == "Infinity")
                    {
                        matrixCopy.Mat[i][j] = 0;
                    }
                }
            }
            
            return true;
        }
    }
}