using System;
using System.IO;
using System.Linq;

public partial class Program
{
    // Метод для вычисление результата задачи в зависимости от дня недели.
    private static int MorningWorkout(string dayOfWeek, int firstNumber, int secondNumber)
    {
        string[] days = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

        if (!days.Contains(dayOfWeek))
        {
            return -1;
        }

        firstNumber = firstNumber < 0 ? -firstNumber : firstNumber;
        secondNumber = secondNumber < 0 ? -secondNumber : secondNumber;
        
        switch (dayOfWeek)
        {
            case "Monday": case "Wednesday": case "Friday":
                int oddSum = 0;
                while (firstNumber > 0)
                {
                    int digit = firstNumber % 10;
                    if (digit % 2 != 0)
                    {
                        oddSum += digit;
                    }

                    firstNumber /= 10;
                }

                return oddSum;
            case "Tuesday": case "Thursday":
                int evenSum = 0;
                while (secondNumber > 0)
                {
                    int digit = secondNumber % 10;
                    if (digit % 2 == 0)
                    {
                        evenSum += digit;
                    }

                    secondNumber /= 10;
                }
                
                return evenSum;
            case "Saturday":
                return Math.Max(firstNumber, secondNumber);
            case "Sunday":
                return firstNumber * secondNumber;
            default:
                return -1;
        }
    }
}