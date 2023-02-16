using System;
using System.Collections.Generic;
using System.Linq;

public class Mushroom
{
    private string Name { get; }
    private double Weight { get; }
    private double Diameter { get; }

    private Mushroom(string name, double weight, double diameter)
    {
        Name = name;
        Weight = weight;
        Diameter = diameter;
    }

    public static Mushroom Parse(string line)
    {
        string[] array = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (array.Length != 3)
        {
            throw new ArgumentException("Incorrect input");
        }

        if (!double.TryParse(array[1], out double mass) || !double.TryParse(array[2], out double diameter))
        {
            throw new ArgumentException("Incorrect input");
        }

        if (mass <= 0 || diameter <= 0)
        {
            throw new ArgumentException("Incorrect input");
        }

        return new Mushroom(array[0], mass, diameter);
    }

    public static double GetMinimalDiameter(List<Mushroom> mushrooms, double m)
    {
        var list = mushrooms.Where(x => x.Weight < m).ToList();
        if (list.Count == 0)
        {
            return 0;
        }

        double minDiameter = double.MaxValue;
        foreach (var element in list)
        {
            if (element.Diameter < minDiameter)
            {
                minDiameter = element.Diameter;
            }
        }

        return minDiameter;
    }
}