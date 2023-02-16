using System;
using System.IO;

internal partial class Program
{
    private static int CountUpperAndLowerCaseLetters(string inputPath)
    {
        var read = new StreamReader(inputPath);
        string text = read.ReadToEnd();

        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        string alphabetUpper = alphabet.ToUpper();
        
        int lowerCase = 0;
        int upperCase = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (alphabet.Contains(text[i]))
            {
                lowerCase += 1;
            }

            if (alphabetUpper.Contains(text[i]))
            {
                upperCase += 1;
            }
        }

        return lowerCase - upperCase;
    }

    private static void WriteOutput(string outputPath, int output)
    {
        using var sw = new StreamWriter(outputPath);
        {
            sw.Write(output);
        }
    }
}