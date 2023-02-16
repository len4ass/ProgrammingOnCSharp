using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using static UserDb;

internal class Server
{
    private class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsBanned { get; set; }
        public DateTime LoginTime { get; set; }

        public string SignIn(string password, DateTime action)
        {
            if (IsBanned)
            {
                return $"{Name}> account blocked due suspicious login attempt";
            }

            if (IsLoggedIn && (action - LoginTime).TotalHours <= 24)
            {
                IsBanned = true;
                return $"{Name}> account blocked due suspicious login attempt";
            }
            
            if (password != Password)
            {
                if ((action - LoginTime).TotalHours <= 1)
                {
                    IsBanned = true;
                    return $"{Name}> account blocked due suspicious login attempt";
                }
                
                return $"{Name}> incorrect password";
            }

            LoginTime = action;
            IsBanned = false;
            IsLoggedIn = true;
            return $"{Name}> sign in successful";
        }

        public string SignOut(string name, DateTime action)
        {
            if (IsBanned)
            {
                return $"{Name}> account blocked due suspicious login attempt";
            }

            if (IsLoggedIn)
            {
                IsLoggedIn = false;
            }
            
            return $"{Name}> sign out successful";
        }
    }

    private static List<User> _users;
    private static List<string> _writeList;
    
    static Server()
    {
        _users = new List<User>();
        foreach (var user in Users)
        {
            var (name, pass) = user;
            _users.Add(new User {Name = name, Password = pass, IsLoggedIn = false, LoginTime = new DateTime(1978, 2, 3)});
        }

        _writeList = new List<string>();
    }

    private static (bool exists, User user) UserExists(string name)
    {
        foreach (var user in _users)
        {
            if (user.Name == name)
            {
                return (true, user);
            }
        }

        return (false, null);
    }

    private static List<string> ReadFile(string path)
    {
        using var sr = new StreamReader(path);
        var list = new List<string>();

        while (!sr.EndOfStream)
        {
            list.Add(sr.ReadLine());
        }

        return list;
    }

    private static void WriteFile(string path, string buffer)
    {
        using var sw = new StreamWriter(path);
        sw.Write(buffer);
    }
    
    private static void AnalyzeFile(string path)
    {
        var result = ReadFile(path);
        foreach (var line in result)
        {
            string[] split = line.Split(" ");
            var type = split[0];
            if (type == "SI")
            {
                var name = split[1];
                var pass = split[2];
                var date = DateTime.Parse(split[3] + " " + split[4], new CultureInfo("ru-RU"));

                var (exists, user) = UserExists(name);
                if (!exists)
                {
                    _writeList.Add($"{name}> no user with such login");
                    continue;
                }

                var write = user.SignIn(pass, date);
                _writeList.Add(write);
            }
            else if (type == "SO")
            {
                var name = split[1];
                var date = DateTime.Parse(split[2] + " " + split[3], new CultureInfo("ru-RU"));

                var (exists, user) = UserExists(name);
                if (!exists)
                {
                    _writeList.Add($"{name}> no user with such login");
                    continue;
                }

                var write = user.SignOut(name, date);
                _writeList.Add(write);
            }
        }
    }

    public static void ProcessAuthorization(string requestsPath, string requestsResultsPath)
    {
        AnalyzeFile(requestsPath);
        WriteFile(requestsResultsPath, string.Join("\n", _writeList));
    }
}