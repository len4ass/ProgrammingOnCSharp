using System.Linq;

public partial class Program
{
    private static int[] ParseInput(string input)
    { 
        string[] inputSplit = input.Split(" ");
        int[] values = new int[inputSplit.Length];

        for (int i = 0; i < inputSplit.Length; i++)
        {
            int.TryParse(inputSplit[i], out values[i]);
        }

        return values;
    }

    private static int GetMaxInArray(int[] numberArray)
    {
        return numberArray.Max();
    }
}