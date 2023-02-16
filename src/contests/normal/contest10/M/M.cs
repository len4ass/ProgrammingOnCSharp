using System;
using System.Collections.Generic;

internal class Map<TKey, TValue>
{
    private List<(TKey, TValue)> _map = new();
    
    public Map()
    {
    }
    
    public int Count => _map.Count;
    
    public TValue this[TKey key]
    {
        get
        {
            foreach (var element in _map)
            {
                var (currentKey, currentValue) = element;
                if (currentKey.Equals(key))
                {
                    return currentValue;
                }
            }

            throw new ArgumentException($"The given key '{key}' was not present in the map.");
        }
    }
    
    public bool ContainsKey(TKey key)
    {
        foreach (var element in _map)
        {
            var (currentKey, _) = element;
            if (currentKey.Equals(key))
            {
                return true;
            }
        }

        return false;
    }
    
    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
        {
            throw new ArgumentException($"An item with the same key has already been added. Key: {key}");
        }
        
        _map.Add((key, value));
    }
    
    public bool Remove(TKey key)
    {
        foreach (var element in _map)
        {
            var (currentKey, currentValue) = element;
            if (currentKey.Equals(key))
            {
                _map.Remove((currentKey, currentValue));
                return true;
            }
        }

        return false;
    }
}