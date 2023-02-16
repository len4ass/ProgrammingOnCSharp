using System;

public partial class Program
{
    private static int[][] GetBellTriangle(uint rowCount)
    {
        int[][] bell = new int[(int)rowCount][];
        for (int i = 0; i < rowCount; i++)
        {
            bell[i] = new int[i + 1];
        }

        bell[0][0] = 1;
        for (int i = 1; i < bell.Length; i++)
        {
            bell[i][0] = bell[i - 1][i - 1];
            for (int j = 1; j < bell[i].Length; j++)
            {
                bell[i][j] = bell[i][j - 1] + bell[i - 1][j - 1];
            }
        }

        return bell;
    }

    private static void PrintJaggedArray(int[][] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                if (j == array[i].Length - 1)
                {
                    Console.Write(array[i][j] + "\n");
                    break;
                }
                
                Console.Write(array[i][j] + " ");
            }
        }
    }
}