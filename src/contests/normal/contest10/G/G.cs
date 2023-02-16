using System.IO;

internal class Program
{
    private static void Main()
    {
        using var sr = new StreamReader("input.txt");
        string buffer = sr.ReadToEnd();
        var lines = buffer.Split("\n");
        
        var amount = ushort.Parse(lines[0]);
        var numbers = new ushort[amount + 1];
        numbers[0] = amount;
        
        for (int i = 1; i < amount + 1; i++)
        {
            numbers[i] = ushort.Parse(lines[i]);
        }

        using var s = new FileStream("output.bin", FileMode.OpenOrCreate);
        using var sw = new BinaryWriter(s);
        foreach (var number in numbers)
        {
            sw.Write(number);
        }
    }
}