using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

internal sealed class ReplacedString
{
    private string toSwap;
    private string initial;
    private string final;

    // KMP algo
    public static int[] SearchString(string str, string substr)
    {
        var retArr = new List<int>();
        int m = substr.Length;
        int n = str.Length;
        int i = 0;
        int j = 0;
        
        int[] lps = new int[m];
        GetArray(substr, m, lps);

        while (i < n)
        {
            if (substr[j] == str[i])
            {
                j++;
                i++;
            }

            if (j == m)
            {
                retArr.Add(i - j);
                j = lps[j - 1];
            }

            else if (i < n && substr[j] != str[i])
            {
                if (j != 0)
                {
                    j = lps[j - 1];

                }
                else
                {
                    i += 1;
                }
            }
        }

        return retArr.ToArray();
    }

    private static void GetArray(string substr, int m, int[] lps)
    {
        int len = 0;
        int i = 1;

        lps[0] = 0;

        while (i < m)
        {
            if (substr[i] == substr[len])
            {
                len++;
                lps[i] = len;
                i++;
            }
            else
            {
                if (len != 0)
                {
                    len = lps[len - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }
    }
    
    public ReplacedString(string s, string initialSubstring, string finalSubstring)
    {
        toSwap = s;
        initial = initialSubstring;
        final = finalSubstring;
    }

    public override string ToString()
    {
        string previous = toSwap;
        toSwap = toSwap.Replace(initial, final);
        
        while (toSwap != previous)
        {
            previous = toSwap;
            toSwap = toSwap.Replace(initial, final);
        }
        
        return toSwap;
    }
}