using System;

internal class ChristmasArray : BaseArray
{
    public ChristmasArray(int[] array) : base(array)
    {
        
    }
    
    public override int this[int number]
    {
        get
        {
            int closest = int.MaxValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] - number > 0 && array[i] < closest)
                {
                    closest = array[i];
                }
            }

            if (closest != int.MaxValue)
            {
                return closest;
            }

            throw new ArgumentException("Number does not exist.");
        }
    }

    public override double GetMetric()
    {
        int even = 0;
        for (int i = 0; i < array.Length; i++)
        {
            var element = array[i];
            if (element % 2 == 0)
            {
                even += 1;
            }
        }
        
        int sum = 0;
        for (int i = 0; i < array.Length; i++)
        {
            var element = array[i];
            string repr = element.ToString();
            sum += repr.Length;
        }
        
        return (double)even / sum;
    }
}