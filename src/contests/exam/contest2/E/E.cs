using System;

public class Field
{
    private int[,] matrix;

    public Field(int[,] matrix)
    {
        this.matrix = matrix;
    }

    public void FillIn(string fillType)
    {
        if (fillType != "top to bottom" && fillType != "bottom to top")
        {
            throw new ArgumentException("Incorrect input");
        }

        if (fillType == "top to bottom")
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = i + j + 1;
                }
            }

            return;
        }

        if (fillType == "bottom to top")
        {
            int prevStartCounter = 1;
            int counter = 1;
            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == matrix.GetLength(1) - 1)
                    {
                        matrix[i, j] = counter;
                        prevStartCounter += 1;
                        counter = prevStartCounter;
                        continue;
                    }
                    
                    matrix[i, j] = counter;
                    counter += 1;
                }
            }
        }
    }

    public override string ToString()
    {
        string matrixString = string.Empty;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (i == matrix.GetLength(0) - 1 && j == matrix.GetLength(1) - 1)
                {
                    matrixString += matrix[i, j];
                    continue;
                }

                if (j == matrix.GetLength(1) - 1)
                {
                    matrixString += matrix[i, j] + "\n";
                    continue;
                }

                matrixString += matrix[i, j] + " ";
            }
        }

        return matrixString;
    }
}