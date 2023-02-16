using System;
using System.Collections.Generic;

internal static class Program
{
    private static bool SpiralIsCrossed(int[] array, int l)
    {
        if (l > 401)
        {
            return false;
        }
        
        int x = 0;
        int y = 0;
        var wasThere = new Dictionary<Tuple<int, int>, bool>();
        
        for (int i = 0; i < l; i++)
        {
            int prevX = x;
            int prevY = y;

            if (i % 4 == 0)
            {
                y += array[i];
                
                for (int j = prevY; i == l - 1 ? j <= y : j < y; j++)
                {
                    if (!wasThere.TryAdd(new Tuple<int, int>(x, j), true))
                    {
                        return true;
                    }
                }
            }
            else if (i % 4 == 1)
            {
                x -= array[i];

                for (int j = prevX; i == l - 1 ? j >= x : j > x; j--)
                {
                    if (!wasThere.TryAdd(new Tuple<int, int>(j, y), true))
                    {
                        return true;
                    }
                }
            }
            else if (i % 4 == 2)
            {
                y -= array[i];

                for (int j = prevY; i == l - 1 ? j >= y : j > y; j--)
                {
                    if (!wasThere.TryAdd(new Tuple<int, int>(x, j), true))
                    {
                        return true;
                    }
                }
            }
            else if (i % 4 == 3)
            {
                x += array[i];

                for (int j = prevX; i == l - 1 ? j <= x : j < x; j++)
                {
                    if (!wasThere.TryAdd(new Tuple<int, int>(j, y), true))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private static void Main()
    {
        int[] array = Array.ConvertAll(Console.ReadLine()!.Split(' '), int.Parse);
        int l = array.Length;
        
        Console.WriteLine(SpiralIsCrossed(array, l) ? "Cross" : "No cross");
    }
}