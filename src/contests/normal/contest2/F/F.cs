using System;
using System.Collections.Generic;
using System.Linq;

public partial class Program
{
    // Проверка входного числа на корректность.
    private static bool Validate(int n)
    {
        return n > 0;
    }

    // Сумма делителей числа N (делители строго меньше N).
    private static int DivisorsSum(int n)
    {
        if (n == 1)
        {
            return 0;
        }
        
        int upperBound = (int)Math.Sqrt(n) + 1;
        var setOfDivs = new SortedSet<int>();

        for (int i = 1; i < upperBound; i++)
        {
            if (n % i == 0)
            {
                setOfDivs.Add(i);
                if (n / i != n)
                {
                    setOfDivs.Add(n / i);
                }
            }
        }

        return setOfDivs.Sum();
    }
}