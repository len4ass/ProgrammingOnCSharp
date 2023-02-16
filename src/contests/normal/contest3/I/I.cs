using System;

public static class Program
{
    public static void Main(string[] args)
    {
        string line = Console.ReadLine();
        if (line is null || line.Length == 0)
        {
            return;
        }
        
        string[] lineSplit = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
        int[] values = new int[lineSplit.Length];
        for (int i = 0; i < values.Length; i++)
        {
            if (!int.TryParse(lineSplit[i], out values[i]))
            {
                return;
            }
        }

        for (int i = 0; i < values.Length; i++)
        {
            int shiftToLeftWithRangeLimit = i % values.Length;
            
            int[] valsLeft = values[shiftToLeftWithRangeLimit..];
            int[] valsRight = values[..shiftToLeftWithRangeLimit];
            
            string leftPart = string.Join("", valsLeft);
            string rightPart = string.Join("", valsRight);
            
            Console.WriteLine(leftPart + rightPart);
        }
    }
}