using System;
using System.Collections.Generic;
using System.IO;

internal sealed class Logger
{
    private static List<string> _logs = new List<string>();
    public static void HandleCommand(string command)
    {
        string currentCommand = String.Empty;
        string textToWrite = String.Empty;

        bool isCommand = true;
        bool isText = false;
        for (int i = 0; i < command.Length; i++)
        {
            if (isCommand)
            {
                if (command[i] == ' ')
                {
                    isCommand = false;
                }
                else
                {
                    currentCommand += command[i];
                }
            }
            else
            {
                if (!isText)
                {
                    if (command[i] == '<')
                    {
                        isText = true;
                    }
                }
                else
                {
                    if (command[i] == '>')
                    {
                        isText = false;
                    }
                    else
                    {
                        textToWrite += command[i];
                    }
                }
            }
        }

        
        if (currentCommand == "AddLog")
        {
            _logs.Add(textToWrite);
        }
        else if (currentCommand == "DeleteLastLog")
        {
            var file = new FileInfo("logs.log");
            
            if (_logs.Count == 0)
            {
                using (StreamWriter sw = file.AppendText())
                {
                    sw.WriteLine("No active logs");
                }
            }
            else
            {
                _logs.RemoveAt(_logs.Count - 1);
            }
        }
        else if (currentCommand == "WriteAllLogs")
        {
            var file = new FileInfo("logs.log");
            
            if (_logs.Count == 0)
            {
                using (StreamWriter sw = file.AppendText())
                {
                    sw.WriteLine("No active logs");
                }
            }
            else
            {
                using (StreamWriter sw = file.AppendText())
                {
                    sw.WriteLine(string.Join("\n", _logs));
                }
                _logs.Clear();
            }
        }
    }
}