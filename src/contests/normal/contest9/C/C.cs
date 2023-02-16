using System;

internal struct Vector : IComparable<Vector>
{
    private int X { get; set; }
    private int Y { get; set; }
    
    public Vector(int x, int y)
    {
        X = x;
        Y = y;
    }

    public double Length => Math.Sqrt(X * X + Y * Y);

    public static Vector Parse(string input)
    {
        var inputSplit = input.Split();
        if (inputSplit.Length != 2)
        {
            throw new ArgumentException("Incorrect vector");
        }

        bool xParsed = int.TryParse(inputSplit[0], out int x);
        bool yParsed = int.TryParse(inputSplit[1], out int y);
        if (!xParsed || !yParsed)
        {
            throw new ArgumentException("Incorrect vector");
        }
        
        return new Vector(x, y);
    }

    public int CompareTo(Vector other)
    {
        double thisLen = Math.Sqrt(X * X + Y * Y);
        double otherLen = Math.Sqrt(other.X * other.X + other.Y * other.Y);
        
        if (thisLen > otherLen)
        {
            return 1;
        }

        if (thisLen == otherLen)
        {
            return 0;
        }

        return -1;
    }
}