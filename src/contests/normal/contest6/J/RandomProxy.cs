using System;
using System.Collections.Generic;
using System.IO;

class RandomProxy
{
    private StreamWriter _log;
    private static Dictionary<string, int> _user = new();
    private static Random _random = new Random(1579);

    public RandomProxy(StreamWriter log)
    {
        _log = log;
    }

    public void Register(string login, int age)
    {
        if (!_user.TryAdd(login, age))
        {
            throw new ArgumentException($"User {login}: login is already registered");
        }
        
        _log.WriteLine($"User {login}: login registered");
    }

    public int Next(string login)
    {
        if (!_user.ContainsKey(login))
        {
            throw new ArgumentException($"User {login}: login is not registered");
        }
        
        int number;
        if (_user[login] < 20)
        {
            number = _random.Next(0, 1000);
        }
        else
        {
            number = _random.Next(0, Int32.MaxValue);
        }
        
        _log.WriteLine($"User {login}: generate number {number}");
        return number;
    }

    public int Next(string login, int maxValue)
    {
        if (!_user.ContainsKey(login))
        {
            throw new ArgumentException($"User {login}: login is not registered");
        }

        if (_user[login] < 20 && maxValue > 1000)
        {
            throw new ArgumentOutOfRangeException($"User {login}: random bounds out of range");
        }
        
        int number = _random.Next(0, maxValue);
        _log.WriteLine($"User {login}: generate number {number}");
        
        return number;
    }

    public int Next(string login, int minValue, int maxValue)
    {
        if (!_user.ContainsKey(login))
        {
            throw new ArgumentException($"User {login}: login is not registered");
        }

        if (_user[login] < 20 && maxValue - minValue > 1000)
        {
            throw new ArgumentOutOfRangeException($"User {login}: random bounds out of range");
        }
        
        int number = _random.Next(minValue, maxValue);
        _log.WriteLine($"User {login}: generate number {number}");
        
        return number;
    }
}