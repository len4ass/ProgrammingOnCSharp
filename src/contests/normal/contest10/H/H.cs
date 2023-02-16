using System;
using System.IO;
using System.Linq;
using System.Text;

internal class Summator
{
    private readonly string _path;
    
    public Summator(string path)
    {
        _path = path;
    }

    private int GetSum()
    {
        using var sr = new StreamReader(_path, Encoding.UTF8);
        string buffer = sr.ReadToEnd();

        var lines = buffer.Split("\n");
        var sum = 0;
        foreach (var line in lines)
        {
            var split = line.Split("_");
            var numbers = Array.ConvertAll(split, int.Parse);

            sum += numbers.Sum();
        }

        return sum;
    }
    
    public int Sum => GetSum();
}