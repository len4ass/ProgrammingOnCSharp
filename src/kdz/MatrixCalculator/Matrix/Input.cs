using System;

namespace Matrix
{
    /// <summary>
    /// Класс для работы с вводом.
    /// </summary>
    public class Input
    {
        private static readonly string[] MatrixOperations =
        {
            "Найти след матрицы", "Найти транспонированную матрицу", "Найти сумму двух матриц", 
            "Найти разность двух матриц", "Найти произведение двух матриц", "Найти произведение матрицы и числа", 
            "Найти определитель матрицы", "Найти решения системы линейных алгебраических уравнений"
        };

        private static readonly string[] MatrixInputTypes =
        {
            "Ввести с консоли", "Ввести с файла", "Сгенерировать программой"
        };

        private static string _readFileName;
        private static int _matrixOperationNumber;
        private static int _matrixInputNumber;

        /// <summary>
        /// Метод для преобразования строки в целое число с ограничением снизу и сверху.
        /// </summary>
        /// <param name="input">Строка, которая будет конвертирована в число.</param>
        /// <param name="min">Нижняя граница.</param>
        /// <param name="max">Верхняя граница.</param>
        /// <param name="value">После вызова метода передает числовое значение по ссылке.</param>
        /// <returns><c>true</c>, если удалось преобразовать строку в число в диапазоне; иначе <c>false</c></returns>
        private static bool GetLimitedValue(string input, int min, int max, out int value)
        {
            if (!int.TryParse(input, out value))
            {
                Console.WriteLine("Ваш ввод нельзя распознать как число! Повторите попытку.\n");
                return false;
            }

            if (value < min || value > max)
            {
                Console.WriteLine("Ваше число не попало в допустимый диапазон! Повторите попытку.\n");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Метод для передачи типа операции в поле класса.
        /// </summary>
        public static void GetMatrixOperation()
        {
            Console.WriteLine("\nДобро пожаловать в калькулятор матриц! Выберите операцию для матрицы (от 1 до 8).");
            Console.WriteLine($"1. {MatrixOperations[0]}\n" +
                              $"2. {MatrixOperations[1]}\n" +
                              $"3. {MatrixOperations[2]}\n" +
                              $"4. {MatrixOperations[3]}\n" +
                              $"5. {MatrixOperations[4]}\n" +
                              $"6. {MatrixOperations[5]}\n" +
                              $"7. {MatrixOperations[6]}\n" +
                              $"8. {MatrixOperations[7]}\n");

            while (true)
            {
                // Контроль ввода типа матричной операции.
                Console.Write("Ввод: ");
                if (!GetLimitedValue(Console.ReadLine(), 1, 8, out _matrixOperationNumber))
                {
                    continue;
                }

                Console.WriteLine($"Ваш выбор: {MatrixOperations[_matrixOperationNumber - 1].ToLower()}");
                break;
            }
        }

        /// <summary>
        /// Метод для передачи типа ввода в поле класса.
        /// </summary>
        public static void GetMatrixInputType()
        {
            Console.WriteLine("\nВыберите тип ввода матрицы (от 1 до 3).");
            Console.WriteLine($"1. {MatrixInputTypes[0]}\n" +
                              $"2. {MatrixInputTypes[1]}\n" +
                              $"3. {MatrixInputTypes[2]}\n");
            
            while (true)
            {
                // Контроль ввода типа обработки.
                Console.Write("Ввод: ");
                if (!GetLimitedValue(Console.ReadLine(), 1, 3, out _matrixInputNumber))
                {
                    continue;
                }

                Console.WriteLine($"Ваш выбор: {MatrixInputTypes[_matrixInputNumber - 1].ToLower()}");
                break;
            }
        }

        /// <summary>
        /// Создает instance класса <c>ConsoleHandle</c> и пытается получить число из консоли.
        /// </summary>
        /// <param name="number">После вызова метода передает числовое значение по ссылке.</param>
        private static void GetNumberFromConsole(out double number)
        {
            var console = new ConsoleHandle();
            console.RunConsoleHandler(out number);
        }

        /// <summary>
        /// Создает instance класса <c>ConsoleHandle</c> и и пытается получить объект класса Matrix из консоли.
        /// </summary>
        /// <param name="matrix">После вызова метода передает объект типа Matrix по ссылке</param>
        private static void GetMatrixFromConsole(out Matrix matrix)
        {
            var console = new ConsoleHandle();
            console.RunConsoleHandler(out matrix);
        }

        /// <summary>
        /// Создает instance класса <c>FileHandle</c> и пытаеся получить число из файла <c>fileName</c>.
        /// </summary>
        /// <param name="fileName">Строка, содержащая имя файла для создания объекта класса.</param>
        /// <param name="number">После вызова метода передает числовое значение по ссылке.</param>
        /// <returns><c>true</c>, если удалось получить значение; иначе <c>false</c></returns>
        private static bool GetNumberFromFile(string fileName, out double number)
        {
            var read = new FileHandle(fileName);
            if (!read.GetNumber(out number))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Создает instance класса <c>FileHandle</c> и пытаеся получить объект типа <c>Matrix</c> из файла <c>fileName</c>.
        /// </summary>
        /// <param name="fileName">Строка, содержащая имя файла для создания объекта класса <c>FileHandle</c>.</param>
        /// <param name="matrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        /// <returns><c>true</c>, если удалось получить объект; иначе <c>false</c></returns>
        public static bool GetMatrixFromFile(string fileName, out Matrix matrix)
        {
            var read = new FileHandle(fileName);
            if (!read.GetMatrix(out matrix))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Создает instance класса <c>GenerateHandle</c> и пытаеся получить объект типа <c>Matrix</c>.
        /// </summary>
        /// <param name="matrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        public static void GetMatrixFromGenerator(out Matrix matrix)
        {
            var generate = new GenerateHandle();
            generate.RunGeneratorHandler(out matrix);
        }

        /// <summary>
        /// Метод для передачи имени введенного файла в поле класса.
        /// </summary>
        /// <param name="valueType">Хранит тип вводимых данных в виде строки (матрица/число).</param>
        private static void GetFileName(string valueType)
        {
            while (true)
            {
                Console.WriteLine($"\nВведите название файла, чтобы получить {valueType} (без расширения): ");
                Console.Write("Ввод: ");
                string fileName = Console.ReadLine();

                if (fileName is null || fileName.Length == 0)
                {
                    Console.WriteLine("\nНе удалось получить имя вашего файла! Повторите попытку.");
                    continue;
                }

                _readFileName = fileName;
                break;
            }
        }

        /// <summary>
        /// Получает число в зависимости от типа ввода.
        /// </summary>
        /// <param name="number">После вызова метода передает числовое значение по ссылке.</param>
        private static void GetNumber(out double number)
        {
            number = 0;
            
            // Рассмотрение разных типов обработки.
            switch (_matrixInputNumber)
            {
                case 1:
                    GetNumberFromConsole(out number);
                    Console.WriteLine($"\nВаше число из консоли: {number}\n");
                    break;
                case 2:
                    while (true)
                    {
                        GetFileName("число");
                        Console.WriteLine($"\nУбедитесь, что число в файле {_readFileName}.txt корректно " +
                                          "и нажмите ENTER.");
                        Console.Write("Ввод: ");
                        Console.ReadLine();

                        // Если не удалось корректно обработать файл, то спрашиваем ввод файла заново.
                        if (!GetNumberFromFile(_readFileName, out number))
                        {
                            continue;
                        }
                        
                        break;
                    }
                    Console.WriteLine($"\nВаше число из файла {_readFileName}.txt: {number}\n");
                    break;
                case 3:
                    GetNumberFromConsole(out number);
                    Console.WriteLine($"\nВаше число из консоли: {number}\n");
                    break;
            }
        }

        /// <summary>
        /// Получает объект типа <c>Matrix</c> в зависимости от типа обработки.
        /// </summary>
        /// <param name="matrix">После вызова метода передает объект типа <c>Matrix</c> по ссылке.</param>
        private static void GetMatrix(out Matrix matrix)
        {
            matrix = new Matrix(1, 1);
            
            // Рассмотрение разных типов обработки.
            switch (_matrixInputNumber)
            {
                case 1:
                    GetMatrixFromConsole(out matrix);
                    Console.WriteLine("\nМатрица из консоли: ");
                    matrix.Print();
                    break;
                case 2:
                    while (true)
                    {
                        GetFileName("матрицу");
                        Console.WriteLine($"\nУбедитесь, что матрица в файле {_readFileName}.txt корректна " +
                                          "и нажмите ENTER.");
                        Console.Write("Ввод: ");
                        Console.ReadLine();

                        // Если не удалось корректно обработать файл, то спрашиваем объект заново.
                        if (!GetMatrixFromFile(_readFileName, out matrix))
                        {
                            continue;
                        }
                        
                        break;
                    }
                    Console.WriteLine($"\nМатрица из файла {_readFileName}.txt: ");
                    matrix.Print();
                    break;
                case 3:
                    GetMatrixFromGenerator(out matrix);
                    Console.WriteLine("\nСгенерированная матрица: ");
                    matrix.Print();
                    break;
            }
        }

        /// <summary>
        /// Вызываем метод <c>GetTrace</c> класса <c>Matrix</c> для объекта, полученного из <c>GetMatrix</c>.
        /// </summary>
        private static void Trace()
        {
            // Получаем след матрицы.
            switch (_matrixOperationNumber)
            {
                case 1:
                    GetMatrix(out Matrix traceMatrix);
                    if (!traceMatrix.GetTrace(out double trace))
                    {
                        Console.WriteLine("\nНевозможно получить след данной матрицы.");
                        break;
                    }
                    
                    Console.WriteLine($"\nСлед матрицы: {trace}");
                    break;
            }
        }

        /// <summary>
        /// Вызываем метод <c>GetTranspose</c> класса <c>Matrix</c> для объекта, полученного из <c>GetMatrix</c>.
        /// </summary>
        public static void Transpose()
        {
            // Получаем транспозицию матрицы.
            switch (_matrixOperationNumber)
            {
                case 2:
                    GetMatrix(out Matrix toTransposeMatrix);
                    if (!toTransposeMatrix.GetTranspose(out Matrix transposedMatrix))
                    {
                        Console.WriteLine("\nНевозможно получить транспозицию данной матрицы.");
                        break;
                    }
                    
                    Console.WriteLine("\nТранспонированная матрица: ");
                    transposedMatrix.Print();
                    break;
            }
        }

        /// <summary>
        /// Вызываем метод <c>GetSum</c> класса <c>Matrix</c> для объекта, полученного из <c>GetMatrix</c>.
        /// </summary>
        private static void Sum()
        {
            // Получаем сумму двух матриц.
            switch (_matrixOperationNumber)
            {
                case 3:
                    GetMatrix(out Matrix firstSumMatrix);
                    GetMatrix(out Matrix secondSumMatrix);

                    if (!firstSumMatrix.GetSum(secondSumMatrix, out Matrix sumMatrix))
                    {
                        Console.WriteLine("\nНевозможно получить сумму данных двух матрицы.");
                        break;
                    }
                    
                    Console.WriteLine("\nСумма матриц: ");
                    sumMatrix.Print();
                    break;
            }
        }

        /// <summary>
        /// Вызываем метод <c>GetDifference</c> класса <c>Matrix</c> для объекта, полученного из <c>GetMatrix</c>.
        /// </summary>
        private static void Diff()
        {
            // Получаем разность двух матриц.
            switch (_matrixOperationNumber)
            {
                case 4:
                    GetMatrix(out Matrix firstDiffMatrix);
                    GetMatrix(out Matrix secondDiffMatrix);

                    if (!firstDiffMatrix.GetDifference(secondDiffMatrix, out Matrix diffMatrix))
                    {
                        Console.WriteLine("\nНевозможно получить разность данных двух матриц.");
                        break;
                    }
                    
                    Console.WriteLine("\nРазность матриц: ");
                    diffMatrix.Print();
                    break;
            }
        }

        /// <summary>
        /// Вызываем метод <c>GetProduct</c> класса <c>Matrix</c> для объекта, полученного из <c>GetMatrix</c>.
        /// </summary>
        private static void Prod()
        {
            // Получаем прозведение двух матриц.
            switch (_matrixOperationNumber)
            {
                case 5:
                    GetMatrix(out Matrix firstProdMatrix);
                    GetMatrix(out Matrix secondProdMatrix);
                    
                    if (!firstProdMatrix.GetProduct(secondProdMatrix, out Matrix prodMatrix))
                    {
                        Console.WriteLine("\nНевозможно умножить данные две матрицы.");
                        break;
                    }
                    
                    Console.WriteLine("\nПроизведение матриц: ");
                    prodMatrix.Print();
                    break;
            }
        }
        
        /// <summary>
        /// Вызываем метод <c>GetProductForNumber</c> класса <c>Matrix</c> для объекта, полученного из <c>GetMatrix</c>.
        /// </summary>
        private static void NumProd()
        {
            // Получаем произведение числа и матрицы.
            switch (_matrixOperationNumber)
            {
                case 6:
                    GetMatrix(out Matrix prodNumMatrix);
                    GetNumber(out double fraction);

                    if (!prodNumMatrix.GetProductForNumber(fraction, out prodNumMatrix))
                    {
                        Console.WriteLine("\nНевозможно умножить данную матрицу на число.");
                        break;
                    }
                    
                    Console.WriteLine("\nПроизведение матрицы и числа: ");
                    prodNumMatrix.Print();
                    break;
            }
        }

        /// <summary>
        /// Вызываем метод <c>GetDeterminant</c> класса <c>Matrix</c> для объекта, полученного из <c>GetMatrix</c>.
        /// </summary>
        private static void Determinant()
        {
            // Получаем детерминант матрицы.
            switch (_matrixOperationNumber)
            {
                case 7:
                    GetMatrix(out Matrix detMatrix); 
                    if (!detMatrix.GetDeterminant(out double detOfMatrix))
                    {
                        Console.WriteLine("\nНевозможно получить детерминант (определитель) данной матрицы.");
                        break;
                    }
                    
                    Console.WriteLine($"\nДетерминант (определитель) матрицы: {detOfMatrix}");
                    break;
            }
        }

        /// <summary>
        /// Вызываем метод <c>GetSolutions</c> класса <c>Matrix</c> для объекта, полученного из <c>GetMatrix</c>.
        /// </summary>
        private static void Gauss()
        {
            // Получаем решения СЛАУ методом Гаусса.
            switch (_matrixOperationNumber)
            {
                case 8:
                    GetMatrix(out Matrix gaussMatrix);
                    if (!gaussMatrix.GetSolutions(out Matrix matrix))
                    {
                        Console.WriteLine("\nНевозможно получить решение СЛАУ для данной матрицы.");
                        break;
                    }
                    
                    Console.WriteLine("\nРешения СЛАУ из матрицы: ");
                    matrix.Print();
                    break;
            }
        }
        
        /// <summary>
        /// Вызываем все методы, чтобы отработал тот, в котором будет вход в case.
        /// </summary>
        public static void RunMethod()
        {
            Trace();
            Transpose();
            Sum();
            Diff();
            Prod();
            NumProd();
            Determinant();
            Gauss();
        }

        /// <summary>
        /// Метод для реализации повтора решений.
        /// </summary>
        /// <returns><c>true</c>, если пользователь ввел Y или y; иначе <c>false</c></returns>
        public static bool RunAgain()
        {
            Console.WriteLine("\nХотите начать заново?\n" +
                              "Введите Y или y для того, чтобы начать заново.\n" +
                              "Иначе введите любую другую последовательность символов.");
            Console.Write("Ввод: ");
            
            string input = Console.ReadLine();
            return input == "Y" || input == "y";
        }
    }
}