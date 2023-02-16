using System;

internal static class Program
{
    private static int MaxSum(int[] array, int n)
    {
        int includeElementFirstSum = array[0];
        int excludeElementFirstSum = 0;

        for (int i = 1; i < n - 1; i++)
        {
            int excludeNewElement = Math.Max(includeElementFirstSum, excludeElementFirstSum);

            includeElementFirstSum = excludeElementFirstSum + array[i];
            excludeElementFirstSum = excludeNewElement;
        }

        int includeElementSecondSum = array[1];
        int excludeElementSecondSum = 0;

        for (int i = 2; i < n; i++)
        {
            int excludeNewElement = Math.Max(includeElementSecondSum, excludeElementSecondSum);

            includeElementSecondSum = excludeElementSecondSum + array[i];
            excludeElementSecondSum = excludeNewElement;
        }

        return Math.Max(Math.Max(includeElementFirstSum, excludeElementFirstSum), Math.Max(includeElementSecondSum, excludeElementSecondSum));
    }
    
    private static void Main()
    {
        int[] array = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int arrLen = array.Length;

        if (arrLen == 1)
        {
            Console.WriteLine(array[0]);
        }
        else if (arrLen == 2)
        {
            Console.WriteLine(Math.Max(array[0], array[1]));
        }
        else
        {
            Console.WriteLine(MaxSum(array, arrLen));
        }
    }
}