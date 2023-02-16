using System;
using System.Linq;
using Microsoft.VisualBasic;

public partial class Program
{
    private static bool TryParseInput(string inputA, string inputB, out int a, out int b)
    {
        if (!int.TryParse(inputA, out a) | !int.TryParse(inputB, out b))
        {
            return false;
        }

        if (a < 0 || b < 0)
        {
            return false;
        }

        return true;
    }

    private static void SwapMaxDigit(ref int a, ref int b)
    {
        string strA = a.ToString();
        string strB = b.ToString();

        int tempMaxA = 0;
        int tempMaxB = 0;

        for (int i = 0; i < strA.Length; i++)
        {
            int.TryParse(strA[i].ToString(), out int maxVal);

            if (maxVal > tempMaxA)
            {
                tempMaxA = maxVal;
            }
        }

        for (int i = 0; i < strB.Length; i++)
        {
            int.TryParse(strB[i].ToString(), out int maxVal);

            if (maxVal >= tempMaxB)
            {
                tempMaxB = maxVal;
            }
        }

        strA = strA.Replace(tempMaxA.ToString(), tempMaxB.ToString());
        strB = strB.Replace(tempMaxB.ToString(), tempMaxA.ToString());
        
        int.TryParse(strA, out a);
        int.TryParse(strB, out b);
    }
}