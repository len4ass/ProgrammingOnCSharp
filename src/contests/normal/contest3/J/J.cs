using System;
using System.IO;

public partial class Program
{
    private static string[] ReadCodeLines(string codePath)
    {
        var read = new StreamReader(codePath);
        string lines = read.ReadToEnd();
        string[] split = lines.Split("\n");

        return split;
    }

    private static string[] CleanCode(string[] codeWithComments)
    {
        bool flag = false;
        string allLines = "";
        for (int i = 0; i < codeWithComments.Length; i++)
        {
            string currentLine = codeWithComments[i];
            string currentLineFixed = currentLine.Trim(' ');

            if (!flag && currentLineFixed.StartsWith("//"))
            {
                continue;
            }

            if (!flag && currentLineFixed.StartsWith("/*"))
            {
                flag = true;
            }

            if (flag && currentLineFixed.EndsWith("*/"))
            {
                flag = false;
                continue;
            }

            if (flag)
            {
                continue;
            }

            allLines += currentLine + "\n";
        }
        
        string[] split = allLines.Split("\n");
        return split;
    }

    private static void WriteCode(string codeFilePath, string[] codeLines)
    {
        using (var sw = new StreamWriter(codeFilePath))
        {
            for (int i = 0; i < codeLines.Length; i++)
            {
                Console.WriteLine(codeLines[i]);
                sw.WriteLine(codeLines[i]);
            }
        }
        
    }
}