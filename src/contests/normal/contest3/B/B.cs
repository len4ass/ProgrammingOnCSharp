using System;

public partial class Program
{
    private static void GetLetterDigitCount(string line, out int digitCount, out int letterCount)
    {
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        string alphabetUpper = alphabet.ToUpper();
        string digits = "0123456789";
        digitCount = 0;
        letterCount = 0;

        for (int i = 0; i < line.Length; i++)
        {
            char currentChar = line[i];
            if (alphabet.Contains(currentChar) || alphabetUpper.Contains(currentChar))
            {
                letterCount += 1;
            }

            if (digits.Contains(currentChar))
            {
                digitCount += 1;
            }
        }
    }
}