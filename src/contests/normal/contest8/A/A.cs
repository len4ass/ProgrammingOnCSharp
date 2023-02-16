using System;

internal partial class Program
{
    public static int SecondInArray(int[] arr)
    {
        var len = arr.Length;
        switch (len)
        {
            case 1:
                throw new ArgumentException("Not enough elements");
            case 2:
                return arr[1] > arr[0] ? arr[0] : arr[1];
            default:
                var firstMaxValue = arr[1] > arr[0] ? arr[1] : arr[0];
                var secondMaxValue = arr[1] > arr[0] ? arr[0] : arr[1];

                for (int i = 2; i < len; i++)
                {
                    if (arr[i] >= firstMaxValue)
                    {
                        secondMaxValue = firstMaxValue;
                        firstMaxValue = arr[i];
                    } 
                    else if (arr[i] >= secondMaxValue)
                    {
                        secondMaxValue = arr[i];
                    }
                }

                return secondMaxValue;
        }
    }
}