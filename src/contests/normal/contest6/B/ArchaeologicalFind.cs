using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable

public class ArchaeologicalFind
{
    private readonly int _age;
    private readonly int _weight;
    private readonly string _name;
    private int _index = 0;
    public static int TotalFindsNumber { get; set; }
    private static List<ArchaeologicalFind> l = new ();
    

    public ArchaeologicalFind(int age, int weight, string name)
    {
        if (age <= 0)
        {
            throw new ArgumentException("Incorrect age");
        }

        if (weight <= 0)
        {
            throw new ArgumentException("Incorrect weight");
        }

        if (name == "?")
        {
            throw new ArgumentException("Undefined name");
        }
        
        _age = age;
        _weight = weight;
        _name = name;
        _index = TotalFindsNumber;
        TotalFindsNumber++;
    }
    
    /// <summary>
    /// Добавляет находку в список.
    /// </summary>
    /// <param name="finds">Список находок.</param>
    /// <param name="archaeologicalFind">Находка.</param>
    public static void AddFind(ICollection<ArchaeologicalFind> finds, ArchaeologicalFind archaeologicalFind)
    {
        foreach (var elem in finds)
        {
            if (elem.Equals(archaeologicalFind))
            {
                return;
            }
        }

        finds.Add(archaeologicalFind);
    }


    public override bool Equals(object obj)
    {
        if (obj is ArchaeologicalFind casted)
        {
            if (casted._age == _age && casted._name == _name && casted._weight == _weight)
            {
                return true;
            }
        }

        return false;
    }
    
    public override string ToString()
    {
        return $"{_index} {_name} {_age} {_weight}";
    }
}