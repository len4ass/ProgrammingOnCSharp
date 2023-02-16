using System;
using System.Linq;

public class MyList<T>
{
    private T[] _array;

    private int _count;

    public MyList()
    {
        _array = Array.Empty<T>();
    }

    public MyList(int capacity)
    {
        _array = new T[capacity];
    }

    public int Count => _count;

    public int Capacity => _array.Length;
    
    public void Add(T element)
    {
        if (_array.Length == 0)
        {
            _array = new T[4];
            _array[0] = element;
            _count = 1;

            return;
        }

        if (_count == _array.Length)
        {
            var newArray = new T[_array.Length * 2];

            int i = 0;
            for (; i < _array.Length; i++)
            {
                newArray[i] = _array[i];
            }

            newArray[i] = element;
            _array = newArray;
            _count++;
            return;
        }

        _array[_count] = element;
        _count++;
    }

    public T this[int x]
    {
        get
        {
            if (x < 0 || x >= _count)
            {
                throw new IndexOutOfRangeException();
            }

            return _array[x];
        }
    }

    public void Clear()
    {
        for (int i = 0; i < _count; i++)
        {
            _array[i] = default;
        }

        _count = 0;
    }

    public void RemoveLast()
    {
        if (_count == 0)
        {
            throw new IndexOutOfRangeException();
        }
        
        _array[^1] = default;
        _count--;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
        {
            throw new IndexOutOfRangeException();
        }

        var newArray = new T[_count - 1];
        for (int i = 0, j = 0; i < _count && j < _count - 1; i++)
        {
            if (i == index)
            {
                continue;
            }

            newArray[j] = _array[i];
            j++;
        }

        _array = newArray;
        _count--;
    }

    public T Max()
    {
        if (_count == 0)
        {
            throw new IndexOutOfRangeException();
        }

        try
        {
            return _array.Max();
        }
        catch (Exception)
        {
            throw new NotSupportedException("This operation is not supported for this type");
        }
    }

    public override string ToString()
    {
        string arrayString = string.Empty;
        foreach (var element in _array)
        {
            arrayString += element + " ";
        }

        return arrayString;
    }
}