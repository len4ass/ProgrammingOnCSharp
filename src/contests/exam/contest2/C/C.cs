using System;

internal static class CircleInSquare
{
    public static double CircumFerence(double side)
    {
        return 2 * (side / 2) * Math.PI;
    }

    public static double Square(double side)
    {
        return Math.PI * (side / 2 * (side / 2));
    }

    public static double FreeSpace(double side)
    {
        double sqOfSquare = side * side;
        double sqOfCircle = Square(side);

        return sqOfSquare - sqOfCircle;
    }
}