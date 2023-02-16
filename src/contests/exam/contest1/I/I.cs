using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

internal partial class Program
{
    private static char[] FindLeastFrequentLetters(string line)
    {
        var dict = new Dictionary<char, int>();
        for (int i = 0; i < line.Length; i++)
        {
            if (dict.Keys.Contains(line[i]))
            {
                dict[line[i]] += 1;
            }
            else
            {
                dict.Add(line[i], 1);
            }
        }

        int least = Int32.MaxValue;
        foreach (var key in dict.Keys)
        {
            if (dict[key] < least)
            {
                least = dict[key];
            }
        }

        var list = new List<char>();
        foreach (var key in dict.Keys)
        {
            if (dict[key] == least)
            {
                list.Add(key);
            }
        }

        return list.ToArray();
    }

    private static void ChangeCaseForLeastFrequentLetters(ref string line)
    {
        char[] chars = FindLeastFrequentLetters(line);

        for (int i = 0; i < chars.Length; i++)
        {
            string currentChar = chars[i].ToString();

            if (currentChar.ToUpper() == currentChar)
            {
                line = line.Replace(currentChar, currentChar.ToLower());
            }
            else
            {
                line = line.Replace(currentChar, currentChar.ToUpper());
            }
        }
    }
}