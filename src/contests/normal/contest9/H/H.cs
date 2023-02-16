using System;

class IntegralCalculator
{
    private static double Sin(double left, double right)
    {
        if (left > right)
        {
            return 0;
        }
        
        double integral = 0;
        while (left + Program.EPS <= right)
        {
            double funcValueLeft = Math.Sin(left);
            double funcValueRight = Math.Sin(left + Program.EPS);
            double square = (funcValueLeft + funcValueRight) * Program.EPS / 2;

            integral += square;
            left += Program.EPS;
        }

        if (Math.Abs(left - right) > 1e-9)
        {
            double funcValueLeft = Math.Sin(left);
            double funcValueRight = Math.Sin(right);
            double smallStep = right - left;
            double square = (funcValueLeft + funcValueRight) * smallStep / 2;

            integral += square;
        }

        return integral;
    }

    private static double Cos(double left, double right)
    {
        if (left > right)
        {
            return 0;
        }
        
        double integral = 0;
        while (left + Program.EPS <= right)
        {
            double funcValueLeft = Math.Cos(left);
            double funcValueRight = Math.Cos(left + Program.EPS);
            double square = (funcValueLeft + funcValueRight) * Program.EPS / 2;

            integral += square;
            left += Program.EPS;
        }

        if (Math.Abs(left - right) > 1e-9)
        {
            double funcValueLeft = Math.Cos(left);
            double funcValueRight = Math.Cos(right);
            double smallStep = right - left;
            double square = (funcValueLeft + funcValueRight) * smallStep / 2;

            integral += square;
        }

        return integral;
    }

    private static double Tan(double left, double right)
    {
        if (left > right)
        {
            return 0;
        }
        
        double integral = 0;
        while (left + Program.EPS <= right)
        {
            double funcValueLeft = Math.Tan(left);
            double funcValueRight = Math.Tan(left + Program.EPS);
            double square = (funcValueLeft + funcValueRight) * Program.EPS / 2;

            integral += square;
            left += Program.EPS;
        }

        if (Math.Abs(left - right) > 1e-9)
        {
            double funcValueLeft = Math.Tan(left);;
            double funcValueRight = Math.Tan(right);
            double smallStep = right - left;
            double square = (funcValueLeft + funcValueRight) * smallStep / 2;

            integral += square;
        }

        return integral;
    }
    
    public static void InsertParameter(int param)
    {
        if (param == 0)
        {
            Program.func += Sin;
        }

        if (param == 1)
        {
            Program.func += Cos;
        }

        if (param == 2)
        {
            Program.func += Tan;
        }
    }
}