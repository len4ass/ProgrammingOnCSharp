using System;
using System.IO;

internal sealed class Matrix
{
    private int[][] mat;
    public Matrix(string fileName)
    {
        using var sr = new StreamReader(fileName);
        string buffer = sr.ReadToEnd();
        string[] lines = buffer.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        int rows = lines.Length;
        int columns = 0;
        mat = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            string[] nums = lines[i].Split(";");
            if (i == 0)
            {
                columns = nums.Length;
            }

            mat[i] = new int[columns];

            for (int j = 0; j < columns; j++)
            {
                int.TryParse(nums[j], out mat[i][j]);
            }
        }
    }

    private int GetEvenNumbersSum()
    {
        int sum = 0;
        for (int i = 0; i < mat.Length; i++)
        {
            for (int j = 0; j < mat[i].Length; j++)
            {
                if (mat[i][j] % 2 == 0)
                {
                    sum += mat[i][j];
                }
            }
        }

        return sum;
    }

    public int SumOfEvenElements => GetEvenNumbersSum();

    public override string ToString()
    {
        string toPrint = String.Empty;
        for (int i = 0; i < mat.Length; i++)
        {
            toPrint += string.Join("\t", mat[i]);
            toPrint += "\n";
        }

        return toPrint;
    }
}