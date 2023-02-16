using System;
public class Snowflake
{
    private int n;
    private int k;
    
    public Snowflake(int widthAndHeight, int raysCount)
    {
        if (widthAndHeight % 2 != 1 || widthAndHeight < 1)
        {
            throw new ArgumentException("Incorrect input");
        }
        
        if (raysCount < 4)
        {
            throw new ArgumentException("Incorrect input");
        }

        if ((raysCount & (raysCount - 1)) != 0)
        {
            throw new ArgumentException("Incorrect input");
        }

        n = widthAndHeight;
        k = raysCount;
    }

    public override string ToString()
    {
        char[][] snowflake = new char[n][];
        for (int i = 0; i < n; i++)
        {
            snowflake[i] = new char[n];
            for (int j = 0; j < n; j++)
            {
                snowflake[i][j] = ' ';
            }
        }

        int center = n / 2;
        if (k > 0)
        {
            for (int i = center; i >= 0; i--)
            {
                snowflake[center][i] = '*';
            }

            k--;
        }

        if (k > 0)
        {
            for (int i = center; i < n; i++)
            {
                snowflake[center][i] = '*';
            }

            k--;
        }

        if (k > 0)
        {
            for (int i = center; i >= 0; i--)
            {
                snowflake[i][center] = '*';
            }

            k--;
        }

        if (k > 0)
        {
            for (int i = center; i < n; i++)
            {
                snowflake[i][center] = '*';
            }

            k--;
        }

        int l = center;
        int t = center;
        int r = center;
        int d = center;
        while (l > 0 || t > 0 || r < n - 1 || d < n - 1)
        {
            if (k > 0)
            {
                for (int i = l, j = center; i >= 0 && j < n; i--, j++)
                {
                    snowflake[j][i] = '*';
                }

                k--;
            }
            
            if (k > 0)
            {
                for (int i = l, j = center; i >= 0 && j >= 0; i--, j--)
                {
                    snowflake[j][i] = '*';
                }

                k--;
            }
            
            if (k > 0)
            {
                for (int i = t, j = center; i >= 0 && j < n; i--, j++)
                {
                    snowflake[i][j] = '*';
                }

                k--;
            }
            
            if (k > 0 && t != center)
            {
                for (int i = t, j = center; i >= 0 && j >= 0; i--, j--)
                {
                    snowflake[i][j] = '*';
                }

                k--;
            }
            
            if (k > 0 && r != center)
            {
                for (int i = r, j = center; i < n && j >= 0; i++, j--)
                {
                    snowflake[j][i] = '*';
                }

                k--;
            }
            
            if (k > 0)
            {
                for (int i = r, j = center; i < n && j < n; i++, j++)
                {
                    snowflake[j][i] = '*';
                }

                k--;
            }
            
            if (k > 0 && d != center)
            {
                for (int i = d, j = center; i < n && j < n; i++, j++)
                {
                    snowflake[i][j] = '*';
                }

                k--;
            }

            if (k > 0 && d != center)
            {
                for (int i = d, j = center; i < n && j >= 0; i++, j--)
                {
                    snowflake[i][j] = '*';
                }

                k--;
            }

            l -= 2;
            t -= 2;
            r += 2;
            d += 2;
        }

        string voila = string.Empty;
        for (int i = 0; i < n; i++)
        {
            string row = string.Join(" ", snowflake[i]);

            if (i == n - 1)
            {
                voila += row;
                break;
            }

            voila += row + "\n";
        }
        
        return voila;
    }
}