using System;
using System.Linq;
internal delegate void Calculator(ref double val);

internal class StackCalculator
{
    private static void Sin(ref double val)
    {
        val = Math.Sin(val);
    }

    private static void Cos(ref double val)
    {
        val = Math.Cos(val);
    }

    private static void Tan(ref double val)
    {
        val = Math.Tan(val);
    }

    public static void CreateRules(int[] args)
    {
        args.ToList().ForEach(rule =>
        {
            Program.calculator += rule switch
            {
                0 => Sin,
                1 => Cos,
                2 => Tan,
                _ => Tan
            };
        });
    }
}