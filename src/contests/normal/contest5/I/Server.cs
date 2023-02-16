using System;
using System.Collections.Generic;

internal sealed class Server
{
    private static Server currentInstance;
    private static string _sName;
    private static List<string> _requests = new();

    public Server()
    {
        
    }

    public Server(string name)
    {
        _sName = name;
    }

    public static Server Connect(string name)
    {
        if (_sName is null)
        {
            currentInstance = new Server(name);
            return currentInstance;
        }

        return currentInstance;
    }

    public void Send(string message)
    {
        if (_sName is null)
        {
            throw new ArgumentException("No connected server");
        }

        string task = $"Sending data {message} to server {_sName}";
        _requests.Add(task);
    }

    public void Receive(string message)
    {
        if (_sName is null)
        {
            throw new ArgumentException("No connected server");
        }

        string task = $"Receiving data {message} from server {_sName}";
        _requests.Add(task);
    }

    public void Output()
    {
        if (_sName is null)
        {
            throw new ArgumentException("No connected server");
        }

        if (_requests.Count == 0)
        {
            return;
        }
        
        string result = string.Join("\n", _requests);
        Console.WriteLine(result);

        _requests.Clear();
    }
}