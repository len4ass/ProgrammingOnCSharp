using System;

internal sealed class IntWrapper
{
    private int _number;
    public IntWrapper(int number)
    {
        if (number >= 0)
        {
            _number = number;
        }
        else
        {
            throw new ArgumentException("Number should be non-negative.");
        }
    }

    public int NumberLength => _number.ToString().Length;
}