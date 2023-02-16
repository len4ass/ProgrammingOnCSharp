namespace Matrix 
{
    /// <summary>
    /// Класс для предпарсинга строк.
    /// </summary>
    #nullable enable
    public class Parse
    {
        /// <summary>
        /// Меняет вхождения ',' во всех строках массива на '.'.
        /// </summary>
        /// <param name="input">Ссылка на массив строк, которые требуют предпарсинга.</param>
        /// <returns>Возвращает ссылку на измененный массив.</returns>
        public static string[] Comma(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                // Если есть вхождение, изменяем.
                if (input[i].Contains(','))
                {
                    input[i] = input[i].Replace(',', '.');
                }
            }

            return input;
        }

        /// <summary>
        /// Меняет в строке вхождения ',' на '.'.
        /// </summary>
        /// <param name="input">Строка, которая требует предпарсинге.</param>
        /// <returns></returns>
        public static string Comma(string? input)
        {
            if (input is null)
            {
                input = "";
            }

            // Если есть вхождение, изменяем.
            if (input.Contains(','))
            {
                input = input.Replace(',', '.');
            }

            return input;
        }
    }
}