internal partial class Program
{
    private static int Count(int[] array)
    {
        int count = 0;
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }
                
                int a = array[i] * array[i] - array[j] * array[j];
                int b = array[i] * array[i] - array[i] * array[j] + array[j] * array[j];

                if (a == b)
                {
                    count += 1;
                }
            }
        }

        return count;
    }
}