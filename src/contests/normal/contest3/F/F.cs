using System;
using System.Collections.Generic;

partial class Program
{
    private static int[] ParseInput(string input)
    {
        string[] inputSplit = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        int[] values = new int[inputSplit.Length];
        for (int i = 0; i < values.Length; i++)
        {
            int.TryParse(inputSplit[i], out values[i]);
        }
        
        return values;
    }
    private static int GetNumberOfEqualElements(int[] first, int[] second)
    {
        int count = 0;
        foreach (var element in first)
        {
            foreach (var vasya in second)
            {
                if (element == vasya)
                {
                    count += 1;
                }
            }
        }

        return count;
    }
}