using System;
using System.Collections.Generic;

internal sealed class Polynomial
{
    private int[] _arg;
    
    public Polynomial(int length)
    {
        _arg = new int[length];
    }

    public static bool TryParsePolynomial(string line, out Polynomial polynomial)
    {
        string[] lines = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        int[] vals = new int[lines.Length];
        polynomial = new Polynomial(lines.Length);
        
        for (int i = 0; i < lines.Length; i++)
        {
            if (!int.TryParse(lines[i], out vals[i]))
            {
                return false;
            }

            polynomial._arg[i] = vals[i];
        }

        return true;
    }

    public static Polynomial operator +(Polynomial a, Polynomial b)
    {
        int maxLen = Math.Max(a._arg.Length, b._arg.Length);
        var objectToReturn = new Polynomial(maxLen);
        
        for (int i = 0; i < maxLen; i++)
        {
            if (i < a._arg.Length)
            {
                objectToReturn._arg[i] += a._arg[i];
            }

            if (i < b._arg.Length)
            {
                objectToReturn._arg[i] += b._arg[i];
            }
        }

        return objectToReturn;
    }

    public static Polynomial operator -(Polynomial a, Polynomial b)
    {
        int maxLen = Math.Max(a._arg.Length, b._arg.Length);
        var objectToReturn = new Polynomial(maxLen);
        
        for (int i = 0; i < maxLen; i++)
        {
            if (i < a._arg.Length)
            {
                objectToReturn._arg[i] += a._arg[i];
            }

            if (i < b._arg.Length)
            {
                objectToReturn._arg[i] -= b._arg[i];
            }
        }

        return objectToReturn;
    }

    public static Polynomial operator *(Polynomial a, int n)
    {
        int len = a._arg.Length;
        var objectToReturn = new Polynomial(len);
        
        for (int i = 0; i < len; i++)
        {
            objectToReturn._arg[i] += a._arg[i] * n;
        }

        return objectToReturn;
    }

    public static Polynomial operator *(Polynomial a, Polynomial b)
    {
        int len = a._arg.Length + b._arg.Length;
        var objectToReturn = new Polynomial(len);

        for (int i = 0; i < a._arg.Length; i++)
        {
            for (int j = 0; j < b._arg.Length; j++)
            {
                objectToReturn._arg[i + j] += a._arg[i] * b._arg[j];
            }
        }

        return objectToReturn;
    }

    public override string ToString()
    {
        bool emptyPolynom = true;
        for (int i = 0; i < _arg.Length; i++)
        {
            if (_arg[i] != 0)
            {
                emptyPolynom = false;
                break;
            }
        }

        if (emptyPolynom)
        {
            return "0";
        }

        string[] output = new string[_arg.Length];
        for (int i = _arg.Length - 1; i > -1; i--)
        {
            if (_arg[i] == 0)
            {
                output[i] = string.Empty;
            }
            else if (_arg[i] == 1)
            {
                if (i == 0)
                {
                    output[i] = _arg[i].ToString();
                }
                else if (i == 1)
                {
                    output[i] = "x";
                }
                else
                {
                    output[i] = $"x{i}";
                }
            }
            else
            {
                if (i == 0)
                {
                    output[i] = _arg[i].ToString();
                }
                else if (i == 1)
                {
                    output[i] = $"{_arg[i]}x";
                }
                else
                {
                    output[i] = $"{_arg[i]}x{i}";
                }
            }
        }

        var list = new List<string>();
        foreach (var element in output)
        {
            if (element != string.Empty)
            {
                list.Add(element);
            }
        }

        output = list.ToArray();
        Array.Reverse(output);
        return string.Join(" + ", output);
    }
}