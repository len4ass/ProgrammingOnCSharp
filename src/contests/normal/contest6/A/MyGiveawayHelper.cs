using System;
using System.Collections.Generic;
using System.Linq;

internal class MyGiveawayHelper
{
    private List<string> _logins;
    private List<string> _prizes;
    private int _generationSeed = 1579;

    public MyGiveawayHelper(string[] logins, string[] prizes)
    {
        _logins = logins.ToList();
        _prizes = prizes.ToList();
    }

    public bool HasPrizes => _prizes.Count > 0;

    public int GeneratePseudo(int n)
    {
        int randomizedValue = _generationSeed * _generationSeed % 1000000 / 100;
        _generationSeed = randomizedValue;

        return randomizedValue % n;
    }
    
    
    public (string prize, string login) GetPrizeLogin()
    {
        int random = GeneratePseudo(_logins.Count);
        string user = _logins[random];
        string prize = _prizes[0];
        
        // schizoid contest
        _prizes.RemoveAt(0);
        return (prize, user);
    }
}