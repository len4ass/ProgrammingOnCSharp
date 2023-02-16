using System;
using System.Linq;

internal sealed class Triangle
{
    private double[] sides = new double[3];

    public Triangle(Point a, Point b, Point c)
    {
        var a1 = a;
        var b1 = b;
        var c1 = c;

        sides[0] = Math.Sqrt(
            (b1.GetX() - a1.GetX()) * (b1.GetX() - a1.GetX()) + (b1.GetY() - a1.GetY()) * (b1.GetY() - a1.GetY()));
        sides[1] = Math.Sqrt(
            (c1.GetX() - b1.GetX()) * (c1.GetX() - b1.GetX()) + (c1.GetY() - b1.GetY()) * (c1.GetY() - b1.GetY()));
        sides[2] = Math.Sqrt(
            (c1.GetX() - a1.GetX()) * (c1.GetX() - a1.GetX()) + (c1.GetY() - a1.GetY()) * (c1.GetY() - a1.GetY()));
    }

    public int GetLongestSide()
    {
        return Array.IndexOf(sides, sides.Max());
    }
    
    public void GetNonUsedSides(int mainSide, out int firstSide, out int secondSide)
    {
        firstSide = 1;
        secondSide = 2;

        if (mainSide == 0)
        {
            firstSide = 1;
            secondSide = 2;
        }
        else if (mainSide == 1)
        {
            firstSide = 0;
            secondSide = 2;
        }
        else if (mainSide == 2)
        {
            firstSide = 0;
            secondSide = 1;
        }
    }
    
    public bool GetEqualSides(out int nonEqualSide, out int first, out int second)
    {
        nonEqualSide = 1;
        first = 0;
        second = 0;
        
        if (Math.Abs(sides[0] - sides[1]) < 1e-9)
        {
            first = 0;
            second = 1;
            nonEqualSide = 2;
            return true;
        }
        
        if (Math.Abs(sides[0] - sides[2]) < 1e-9)
        {
            first = 0;
            second = 2;
            nonEqualSide = 1;
            return true;
        }
        
        if (Math.Abs(sides[1] - sides[2]) < 1e-9)
        {
            first = 1;
            second = 2;
            nonEqualSide = 0;
            return true;
        }

        return false;
    }
    
    public double GetPerimeter() => sides.Sum();

    public double GetSquare()
    {
        double halfPerimeter = sides.Sum() / 2;
        double underRoot = halfPerimeter * (halfPerimeter - sides[0]) * (halfPerimeter - sides[1]) *
                           (halfPerimeter - sides[2]);

        return Math.Sqrt(underRoot);
    }

    public bool GetAngleBetweenEqualsSides(out double angle)
    {
        if (!GetEqualSides(out int nonEqualSide, out int first, out int second))
        {
            angle = 0;
            return false;
        }

        double cosAlpha = (sides[first] * sides[first] + sides[second] * sides[second] -
                           sides[nonEqualSide] * sides[nonEqualSide])
                          / (2 * sides[first] * sides[second]);
        angle = Math.Acos(cosAlpha);
        return true;
    }

    public bool GetHypotenuse(out double hypotenuse)
    {
        int biggestSide = GetLongestSide();
        GetNonUsedSides(biggestSide, out int firstSide, out int secondSide);

        hypotenuse = sides[firstSide] * sides[firstSide] + sides[secondSide] * sides[secondSide];
        double toCompare = sides[biggestSide] * sides[biggestSide];
        if (Math.Abs(hypotenuse - toCompare) < 1e-9)
        {
            hypotenuse = Math.Sqrt(hypotenuse);
            return true;
        }

        return false;
    }
}