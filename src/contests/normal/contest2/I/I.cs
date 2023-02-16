using System;
using System.Collections.Generic;
using System.Linq;

public partial class Program
{
    // Проверка входных чисел на корректность.
    private static bool Validate(int a)
    {
        return a > 0;
    }

    // Первое совершенное число в диапазоне от a до b, если такого числа нет – вернуть -1.
    private static int GetPerfectNumber(int a)
    {
        int num = a == 1 ? 2 : a;
        while (true)
        {
            int upperBound = (int)Math.Sqrt(num) + 1;
            var setOfDivs = new SortedSet<int>();
            for (int i = 1; i < upperBound; i++)
            {
                if (num % i == 0)
                {
                    setOfDivs.Add(i);

                    if (num / i != num)
                    {
                        setOfDivs.Add(num / i);
                    }
                }
            }

            if (setOfDivs.Sum() == num)
            {
                break;
            }

            num += 1;
        }

        return num;
    }
}