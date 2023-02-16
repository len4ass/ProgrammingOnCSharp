using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        int[][] array = new int[9][];
        for (int i = 0; i < 9; i++)
        {
            array[i] = new int[9];
            array[i] = Array.ConvertAll(Console.ReadLine().Split(" "), int.Parse);
        }

        var rows = new List<string>();
        for (int i = 0; i < 9; i++)
        {
            rows.Add(string.Join("", array[i]));
        }

        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rows.Count; j++)
            {
                if (i != j)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        if (rows[i][k] == rows[j][k])
                        {
                            Console.WriteLine("incorrect");
                            return;
                        }
                    }
                }
            }
            
        }

        var columns = new List<string>();
        for (int j = 0; j < 9; j++)
        {
            string toAdd = string.Empty;
            for (int i = 0; i < 9; i++)
            {
                toAdd += array[i][j];
            }
            
            columns.Add(toAdd);
        }
        
        for (int i = 0; i < columns.Count; i++)
        {
            for (int j = 0; j < columns.Count; j++)
            {
                if (i != j)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        if (columns[i][k] == columns[j][k])
                        {
                            Console.WriteLine("incorrect");
                            return;
                        }
                    }
                }
            }
        }

        Console.WriteLine("correct");
    }
}