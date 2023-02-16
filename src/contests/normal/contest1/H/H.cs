using System;

public class Program
{
    // Метод для поиска char в char[] массиве
    public static int FindInArray(char toLookUp, char[] thisArray)
    {
        int index = -1;
        
        for (int i = 0; i < thisArray.Length; i++)
        {
            if (toLookUp == thisArray[i])
            {
                index = i;
                break;
            }
        }

        return index;
    }
    
    public static void Main()
    {
        string firstNum = Console.ReadLine();
        
        // Говнокодом нельзя назвать тот код, который работает
        char[] array =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };
        char a;

        if (!char.TryParse(firstNum, out a))
        {
            // Если полученная строка не может быть конвертирована в тип char
            // выводим "incorrect input" и завершаем программу
            Console.WriteLine("Incorrect input");
            return;
        }

        int foundIndex = FindInArray(a, array);
        if (foundIndex == -1)
        {
            // Проверка на диапазон
            Console.WriteLine("Incorrect input");
            return;
        }
        
        // Массивы с нуля, а мы то с единицы считаем
        Console.WriteLine(foundIndex + 1);
    } 
}