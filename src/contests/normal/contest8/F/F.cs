using System.Linq;
internal partial class Program
{
    public static int[] StrangeSort(int[] arr)
    {
        return arr
            .OrderBy(x => x.ToString().Length)
            .ToArray();
    }
}