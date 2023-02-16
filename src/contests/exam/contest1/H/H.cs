using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

partial class Program
{
    private static void CountInFile(string inputPath, out int countOfEvenLines, out int countOfOddWords)
    {
        countOfEvenLines = 0;
        countOfOddWords = 0;

        char[] symbols = {'.', ',', '?', '!', ':', ';'};
        var read = new StreamReader(inputPath);
        string[] text = File.ReadAllLines(inputPath);

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i].Length % 2 == 0)
            {
                countOfEvenLines += 1;
            }
        }

        var words = new List<string>();
        for (int i = 0; i < text.Length; i++)
        {
            string[] tempWords = text[i].Split(" ");
            for (int j = 0; j < tempWords.Length; j++)
            {
                words.Add(tempWords[j]);
            }
        }

        foreach (string word in words)
        {
            char lastChar = word[^1];
            int wordLength = word.Length;
            if (symbols.Contains(lastChar))
            {
                if ((wordLength - 1) % 2 == 1)
                {
                    countOfOddWords += 1;
                }
            }
            else if (wordLength % 2 == 1)
            {
                countOfOddWords += 1;
            }
        }
    }
}