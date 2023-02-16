using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

internal class ReadWriter
{
    private static int Index(List<(char, int)> list, char check)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var (ch, val) = list[i];
            if (ch == check)
            {
                return i;
            }
        }

        return -1;
    }
    
    private static bool ContainsChar(List<(char, int)> list, char check)
    {
        foreach (var element in list)
        {
            var (ch, _) = element;
            if (ch == check)
            {
                return true;
            }
        }

        return false;
    }
    
    public static (char, char) GetMostAndLeastCommonLetters(string path)
    {
        using var sr = new StreamReader(path, Encoding.UTF8);
        string buffer = sr.ReadToEnd();
        string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var list = new List<(char, int)>();

        foreach (var element in buffer)
        {
            if (chars.Contains(element))
            {
                var lower = element.ToString().ToLower().ToCharArray()[0];
                if (!ContainsChar(list, lower))
                {
                    list.Add((lower, 1));
                    continue;
                }

                int index = Index(list, lower);
                var (ch, count) = list[index];
                list[index] = (ch, count + 1);
            }
        }

        int min = Int32.MaxValue;
        char minchar = char.MinValue;
        
        int max = 0;
        char maxchar = char.MaxValue;
        
        foreach (var element in list)
        {
            var (ch, count) = element;
            if (count > max)
            {
                max = count;
                maxchar = ch;
            }

            if (count < min)
            {   
                min = count;
                minchar = ch;
            }
        }

        return (minchar, maxchar);
    }

    public static void ReplaceMostRareLetter((char, char) leastAndMostCommon, string inputPath, string outputPath)
    {
        var (least, most) = leastAndMostCommon;
        using var sr = new StreamReader(inputPath, Encoding.UTF8);
        using var sw = new StreamWriter(outputPath);
        
        while (sr.Peek() >= 0)
        {
            char character = (char)sr.Read();
            var charLower = character.ToString().ToLower().ToCharArray()[0];
            if (charLower == least)
            {
                if (character == least)
                {
                    sw.Write(most);
                }
                else
                {
                    sw.Write(most.ToString().ToUpper().ToCharArray()[0]);
                }

                continue;
            }
            
            sw.Write(character);
        }
    }
}