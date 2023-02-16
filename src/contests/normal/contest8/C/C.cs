using System.Collections.Generic;
internal delegate void Print(string n);

internal class Logger
{
    private List<(Print, string)> _dList = new();
    
    public void OutputLogs()
    {
        foreach (var print in _dList)
        {
            var (method, line) = print;
            method.Invoke(line);
        }
    }

    public void MakeLog(Print method, string line)
    {
        _dList.Add((method, line));
    }
}